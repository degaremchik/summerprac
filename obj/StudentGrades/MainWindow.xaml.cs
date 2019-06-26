using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;
using Spire.Xls;

namespace StudentGrades
{
    public partial class MainWindow : System.Windows.Window
    {
        List<StudentStatistic> studentStatistics = new List<StudentStatistic>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            dlg.FileName = "STUDENT_MARKS.json";
            dlg.ShowDialog();  
            
            StreamReader stream = new StreamReader(dlg.FileName, Encoding.Default);            
            string JsonText = stream.ReadToEnd();
            stream.Close();

            List<Mark> marks  = JsonConvert.DeserializeObject<List<Mark>>(JsonText);              
            

            List<IGrouping<int, Mark>> students = marks.GroupBy(Grade => Grade.Id).ToList() ;


            //======== For 9 Variant ===========
            //int numberOfTopStudent = 0;
            //foreach (IGrouping<int, Mark> student in students)
            //{

            //    int sumGrades = 0;
            //    foreach (Mark mark in student)
            //    {

            //        if (mark.Grade == "н.д." || mark.Grade == "зв.")
            //        {
            //            mark.Grade = "0";
            //        }
            //        else
            //        {
            //            sumGrades = sumGrades + int.Parse(mark.Grade);
            //        }

            //    }
            //    if (sumGrades / student.Count() >= 90) numberOfTopStudent++;
            //}

            //double result = 100 * Convert.ToDouble(numberOfTopStudent) / Convert.ToDouble(students.Count());
            //label.Content = Math.Round(result, 2) + "% студентов учаться только на оценки \"5\"";
            //dataGrid.ItemsSource = marks;
            // end Code 9 Variant


            //======== For 8 Variant ===========
            foreach (IGrouping<int, Mark> student in students)
            {

                int numOf5 = 0;
                int numOf4 = 0;
                int numOf3 = 0;
                int numOf2 = 0;
                foreach (Mark mark in student)
                {

                    if (mark.Grade == "н.д." || mark.Grade == "зв.")
                    {
                        mark.Grade = "0";
                    }
                    else
                    {
                        if (int.Parse(mark.Grade) >= 90) numOf5++;
                        else if (int.Parse(mark.Grade) >= 75) numOf4++;
                        else if (int.Parse(mark.Grade) >= 60) numOf3++;
                        else { numOf2++; }
                    }
                }

                StudentStatistic studentStatistic = new StudentStatistic();
                studentStatistic.Lastname = student.Last().Lastname;
                studentStatistic.Name = student.Last().Name;
                studentStatistic.GroupNumber = student.Last().GroupNumber;
                studentStatistic.Id = student.Last().Id;
                studentStatistic.Rate5 = (float)Math.Round(100 * (double)numOf5 / student.Count(), 1);
                studentStatistic.Rate4 = (float)Math.Round(100 * (double)numOf4 / student.Count(), 1);
                studentStatistic.Rate3 = (float)Math.Round(100 * (double)numOf3 / student.Count(), 1);
                studentStatistic.Rate2 = (float)Math.Round(100 * (double)numOf2 / student.Count(), 1);
                studentStatistics.Add(studentStatistic);
            }
            dataGrid.ItemsSource = studentStatistics;

            
            
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FileName = "STUDENT_MARKS.xlsx";
            saveFileDialog.ShowDialog();

            string path = saveFileDialog.FileName;

            progressbar.Maximum = studentStatistics.Count;
            progressbar.Value = 50;

            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];

            sheet.Name = "Test";
            sheet.Range["A1"].Text = "ID";
            sheet.Range["B1"].Text = "LastName";
            sheet.Range["C1"].Text = "Name";
            sheet.Range["D1"].Text = "GroupNumber";
            sheet.Range["E1"].Text = "Rate5";
            sheet.Range["F1"].Text = "Rate4";
            sheet.Range["G1"].Text = "Rate3";
            sheet.Range["H1"].Text = "Rate2";
            workbook.Worksheets.Last().Remove();
            workbook.Worksheets.Last().Remove();
            sheet.LastRow = studentStatistics.Count + 1;
            for (int i = 0; i < studentStatistics.Count; i++)
            {
                sheet.Rows[i + 1].Columns[0].NumberValue = studentStatistics[i].Id;
                sheet.Rows[i + 1].Columns[1].Text = studentStatistics[i].Lastname;
                sheet.Rows[i + 1].Columns[2].Text = studentStatistics[i].Name;
                sheet.Rows[i + 1].Columns[3].Text = studentStatistics[i].GroupNumber;
                sheet.Rows[i + 1].Columns[4].NumberValue = studentStatistics[i].Rate5;
                sheet.Rows[i + 1].Columns[5].NumberValue = studentStatistics[i].Rate4;
                sheet.Rows[i + 1].Columns[6].NumberValue = studentStatistics[i].Rate3;
                sheet.Rows[i + 1].Columns[7].NumberValue = studentStatistics[i].Rate2;
                progressbar.Value = i;
            }

            sheet.AutoFitColumn(1);
            sheet.AutoFitColumn(2);
            sheet.AutoFitColumn(3);
            sheet.AutoFitColumn(4);
            sheet.AutoFitColumn(5);
            sheet.AutoFitColumn(6);
            sheet.AutoFitColumn(7);
            sheet.AutoFitColumn(8);

            

            workbook.SaveToFile(path, ExcelVersion.Version2013);
        }
    }
}
