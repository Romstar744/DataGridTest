using DataGrid.Standart.Contracts;
using DataGrid.Standart.Contracts.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGrid
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
        private IPeopleManager peopleManager;
        private BindingSource bindingSource;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Form1" />
        /// </summary>
        public Form1(IPeopleManager peopleManager)
        {
            this.peopleManager = peopleManager;
            bindingSource = new BindingSource();

            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingSource;
        }

        private async void toolStripAdd_Click(object sender, System.EventArgs e)
        {
            var personForm = new PersonForm();
            if (personForm.ShowDialog(this) == DialogResult.OK)
            {
                await peopleManager.AddAsync(personForm.Person);
                bindingSource.ResetBindings(false);
                await SetStats();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ExpelledColumn")
            {
                var data = (Person)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                e.Value = data.Expelled ? "Да" : string.Empty;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "DebtColumn")
            {
                var data = (Person)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                e.Value = data.Debt ? "Да" : string.Empty;
            }
        }

        private async void toolStripDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                var data = (Person)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
                if (MessageBox.Show($"Вы действительно хотите удалить {data.Name}?", "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await peopleManager.DeleteAsync(data.Id);
                    bindingSource.ResetBindings(false);
                    await SetStats();
                }
            }
        }

        private async void toolStripEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                var data = (Person)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
                var personForm = new PersonForm(data);
                if (personForm.ShowDialog(this) == DialogResult.OK)
                {
                    await peopleManager.EditAsync(personForm.Person);
                    bindingSource.ResetBindings(false);
                    await SetStats();
                }
            }
        }

        public async Task SetStats()
        {
            var result = await peopleManager.GetStatsAsync();
            toolStripStatusLabel1.Text = $"Всего: {result.Count}";
            toolStripStatusLabel2.Text = $"{result.FemaleCount} Ж/{result.MaleCount} М";
            toolStripStatusLabel3.Text = $"Отчисленных: {result.ExpelledCount}";
            toolStripStatusLabel4.Text = $"Должников: {result.DebtCount}";
            toolStripStatusLabel5.Text = $"Средняя оценка: {result.AverageRate}";
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            bindingSource.DataSource = await peopleManager.GetAllAsync();
            await SetStats();
        }
    }
}
