using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TransportSolver
{
    public partial class Form1 : Form
    {
        private int[,] _resultAllocation;
        private int[] _supply;
        private int[] _demand;
        private int[,] _costs;

        public Form1()
        {
            InitializeComponent();
            dgvCosts.RowHeadersWidth = 60;
            dgvResult.RowHeadersWidth = 60;
            dgvCosts.AllowUserToAddRows = false; // Чтобы не было пустой строки
            this.Text = "Решение транспортной задачи (Метод мин. элемента)";
        }

        // ============================================================
        // 1. ЗАГРУЗКА ИЗ ФАЙЛА
        // ============================================================
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines = File.ReadAllLines(openFileDialog1.FileName);
                    if (lines.Length < 3) { MessageBox.Show("Файл должен содержать: запасы, потребности и матрицу."); return; }

                    txtSupply.Text = lines[0].Trim();
                    txtDemand.Text = lines[1].Trim();

                    dgvCosts.Rows.Clear();
                    dgvCosts.Columns.Clear();

                    string[] firstRow = lines[2].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    int cols = firstRow.Length;

                    for (int j = 0; j < cols; j++) dgvCosts.Columns.Add($"Col{j}", $"B{j + 1}");

                    for (int i = 2; i < lines.Length; i++)
                    {
                        string[] parts = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == cols) dgvCosts.Rows.Add(parts);
                    }

                    lblStatus.Text = "Статус: Файл загружен. Нажмите 'Рассчитать'.";
                    lblStatus.ForeColor = Color.Green;
                }
                catch (Exception ex) { MessageBox.Show("Ошибка чтения файла: " + ex.Message); }
            }
        }

        // ============================================================
        // 2. РАСЧЕТ МЕТОДОМ МИНИМАЛЬНОГО ЭЛЕМЕНТА
        // ============================================================
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                _supply = txtSupply.Text.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse).ToArray();
                _demand = txtDemand.Text.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse).ToArray();
                int rows = dgvCosts.RowCount;
                int cols = dgvCosts.ColumnCount;

                if (rows == 0 || cols == 0) { MessageBox.Show("Заполните матрицу тарифов!"); return; }
                if (_supply.Length != rows) { MessageBox.Show("Количество запасов не совпадает с числом строк!"); return; }
                if (_demand.Length != cols) { MessageBox.Show("Количество потребностей не совпадает с числом столбцов!"); return; }

                _costs = new int[rows, cols];
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                        _costs[i, j] = Convert.ToInt32(dgvCosts.Rows[i].Cells[j].Value);

                // Проверка баланса (с предупреждением, но считаем)
                int sumSupply = _supply.Sum();
                int sumDemand = _demand.Sum();
                if (sumSupply != sumDemand)
                {
                    MessageBox.Show($"Дисбаланс! Запасы ({sumSupply}) != Потребности ({sumDemand}).\n" +
                                    "Алгоритм отработает, но результат может быть некорректным.", "Предупреждение");
                }

                // ЗАПУСКАЕМ МЕТОД МИНИМАЛЬНОГО ЭЛЕМЕНТА
                _resultAllocation = SolveMinimumCost(_supply, _demand, _costs);

                DisplayResult(_resultAllocation, _costs);

                lblStatus.Text = "Статус: Решение найдено (Метод минимального элемента).";
                lblStatus.ForeColor = Color.DarkGreen;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка в данных: " + ex.Message); }
        }

        // ----- АЛГОРИТМ МИНИМАЛЬНОЙ СТОИМОСТИ (МИНИМАЛЬНОГО ЭЛЕМЕНТА) -----
        private int[,] SolveMinimumCost(int[] supply, int[] demand, int[,] costs)
        {
            int rows = supply.Length;
            int cols = demand.Length;
            int[,] result = new int[rows, cols];

            int[] s = (int[])supply.Clone();
            int[] d = (int[])demand.Clone();

            bool[] rowUsed = new bool[rows];
            bool[] colUsed = new bool[cols];
            int remaining = rows + cols;

            while (remaining > 0)
            {
                int minCost = int.MaxValue;
                int minI = -1, minJ = -1;

                // Ищем НЕЗАЧЕРКНУТУЮ клетку с минимальной ценой
                for (int i = 0; i < rows; i++)
                {
                    if (rowUsed[i]) continue;
                    for (int j = 0; j < cols; j++)
                    {
                        if (colUsed[j]) continue;
                        if (costs[i, j] < minCost)
                        {
                            minCost = costs[i, j];
                            minI = i;
                            minJ = j;
                        }
                    }
                }

                if (minI == -1) break; // Безопасный выход

                int x = Math.Min(s[minI], d[minJ]);
                result[minI, minJ] = x;
                s[minI] -= x;
                d[minJ] -= x;

                // Зачеркиваем строку или столбец, если запас/потребность исчерпаны
                if (s[minI] == 0) { rowUsed[minI] = true; remaining--; }
                if (d[minJ] == 0) { colUsed[minJ] = true; remaining--; }
            }
            return result;
        }

        // ----- ВЫВОД РЕЗУЛЬТАТА (такой же, как был) -----
        private void DisplayResult(int[,] allocation, int[,] costs)
        {
            int rows = allocation.GetLength(0);
            int cols = allocation.GetLength(1);

            dgvResult.Rows.Clear();
            dgvResult.Columns.Clear();

            for (int j = 0; j < cols; j++) dgvResult.Columns.Add($"ResCol{j}", $"B{j + 1}");
            dgvResult.Columns.Add("SumRow", "Итого");

            int totalCost = 0;
            for (int i = 0; i < rows; i++)
            {
                int rowSum = 0;
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvResult);

                for (int j = 0; j < cols; j++)
                {
                    int val = allocation[i, j];
                    row.Cells[j].Value = val;
                    rowSum += val;
                    totalCost += val * costs[i, j];
                }
                row.Cells[cols].Value = rowSum;
                dgvResult.Rows.Add(row);
            }

            DataGridViewRow footerRow = new DataGridViewRow();
            footerRow.CreateCells(dgvResult);
            int colsumm = 0;
            for (int j = 0; j < cols; j++)
            {
                int colSum = 0;
                for (int i = 0; i < rows; i++) colSum += allocation[i, j];
                footerRow.Cells[j].Value = colSum;
                colsumm += colSum; 
            }

            footerRow.Cells[cols].Value = colsumm;
            colsumm = 0;
            footerRow.DefaultCellStyle.Font = new Font("Consolas", 12, FontStyle.Bold);
            dgvResult.Rows.Add(footerRow);

            lblStatus.Text += $" Общая стоимость: {totalCost}";
        }

        // ============================================================
        // 3. СОХРАНЕНИЕ
        // ============================================================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_resultAllocation == null) { MessageBox.Show("Сначала выполните расчет!"); return; }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    sw.WriteLine("=== РЕШЕНИЕ ТРАНСПОРТНОЙ ЗАДАЧИ (Метод мин. элемента) ===");
                    sw.WriteLine($"Запасы: {string.Join(" ", _supply)}");
                    sw.WriteLine($"Потребности: {string.Join(" ", _demand)}");
                    sw.WriteLine("\nМатрица перевозок:");

                    for (int i = 0; i < _resultAllocation.GetLength(0); i++)
                    {
                        string line = "";
                        for (int j = 0; j < _resultAllocation.GetLength(1); j++)
                            line += _resultAllocation[i, j] + "\t";
                        sw.WriteLine(line);
                    }

                    int total = 0;
                    for (int i = 0; i < _resultAllocation.GetLength(0); i++)
                        for (int j = 0; j < _resultAllocation.GetLength(1); j++)
                            total += _resultAllocation[i, j] * _costs[i, j];
                    sw.WriteLine($"\nОбщая стоимость перевозок: {total}");
                }
                MessageBox.Show("Файл сохранен!");
            }
        }

        // ============================================================
        // 4. ОЧИСТКА
        // ============================================================
        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvCosts.Rows.Clear();
            dgvCosts.Columns.Clear();
            dgvResult.Rows.Clear();
            dgvResult.Columns.Clear();
            txtSupply.Text = "";
            txtDemand.Text = "";
            _resultAllocation = null;
            lblStatus.Text = "Статус: Очищено";
            lblStatus.ForeColor = Color.Blue;
        }
    }
}