using DataGrid.Standart.Contracts.Models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DataGrid
{
    public partial class PersonForm : Form
    {
        private Person person;

        public PersonForm(Person person = null)
        {
            this.person = person == null
                ? DataGenerator.CreatePerson(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.Name = "Иванов";
                    x.Gender = Gender.Male;
                    x.BirthDay = DateTime.Now.AddYears(-12);
                })
                : new Person
                {
                    Id = person.Id,
                    Name = person.Name,
                    Gender = person.Gender,
                    BirthDay = person.BirthDay,
                    AvgRate = person.AvgRate,
                    Debt = person.Debt,
                    Expelled = person.Expelled,
                };

            InitializeComponent();

            foreach (var item in Enum.GetValues(typeof(Gender)))
            {
                comboBox1.Items.Add(item);
            }
            if(comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            textBox1.AddBinding(x => x.Text, this.person, x => x.Name, errorProvider1);
            comboBox1.AddBinding(x => x.SelectedItem, this.person, x => x.Gender, errorProvider1);
            dateTimePicker1.AddBinding(x => x.Value, this.person, x => x.BirthDay, errorProvider1);
            numericUpDown1.AddBinding(x => x.Value, this.person, x => x.AvgRate, errorProvider1);
            checkBox1.AddBinding(x => x.Checked, this.person, x => x.Debt, errorProvider1);
            checkBox2.AddBinding(x => x.Checked, this.person, x => x.Expelled, errorProvider1);
        }

        public Person Person => person;

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Silver, 0, 0, Width, 0);
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.FillEllipse(Brushes.Red, 
                new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Height - 4, e.Bounds.Height - 4));
            if(e.Index > -1)
            {
                var value = (Gender)(sender as ComboBox).Items[e.Index];
                e.Graphics.DrawString(GetDisplayValue(value), 
                    e.Font, 
                    new SolidBrush(e.ForeColor), 
                    e.Bounds.X + 20, 
                    e.Bounds.Y);
            }
        }

        private string GetDisplayValue(Gender value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes<DescriptionAttribute>(false);
            return attributes.FirstOrDefault()?.Description ?? "ХЗ";
        }
    }
}
