using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tarea3_Montecarlo
{
    public partial class Form1 : Form
    {
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int experimentos = int.Parse(experimentosTextBox.Text);
                int cantidad = int.Parse(cantidadTextBox.Text);
                int a = int.Parse(minTextBox.Text);
                int b = int.Parse(maxTextBox.Text);

                double result = Montecarlo(experimentos, cantidad, a, b);
                resultLabel.Text = $"Promedio Monte Carlo: {result:F2}";

                // Iteraciones en el datagridview
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Iteration", "Iteración");
                dataGridView1.Columns.Add("Value", "Valor");

                for (int i = 0; i < experimentos; i++)
                {
                    for (int j = 0; j < cantidad; j++)
                    {
                        int value = random.Next(a, b + 1);
                        dataGridView1.Rows.Add(i * cantidad + j + 1, value);
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Introducir solo números válidos, por favor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double Montecarlo(int experimentos, int cantidad, int a, int b)
        {
            double sum = 0;
            List<double> values = new List<double>();

            for (int i = 0; i < experimentos; i++)
            {
                for (int j = 0; j < cantidad; j++)
                {
                    int value = random.Next(a, b + 1);
                    sum += value;
                    values.Add(value);
                }
            }

            double mean = sum / (experimentos * cantidad);

            // Varianza
            double variance = values.Select(val => Math.Pow(val - mean, 2)).Sum() / (experimentos * cantidad);

            // Desviacion estandar
            double stdDev = Math.Sqrt(variance);

            // Muestra desviacion estandar
            stdDevLabel.Text = $"Desviación estándar: {stdDev:F2}";

            return mean;
        }

 
    }
}
