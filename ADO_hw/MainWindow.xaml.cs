using ADO_hw.Data.Entity;
using ADO_hw.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADO_hw
{
    public partial class MainWindow : Window
    {
        private Data.Data data;
        public ObservableCollection<Pair> Pairs { get; set; }

        public ObservableCollection<Data.Entity.Department> DepartmentsView { get; set; }
        private ICollectionView departmentsListView;
        public MainWindow()
        {
            InitializeComponent();

            data = new Data.Data();
            Pairs = new ObservableCollection<Pair>();
            DepartmentsView = new ObservableCollection<Department>();
            this.DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DepartmentCountLabel.Content = data.Departments.Count().ToString();
            ManagerCountLabel.Content = data.Managers.Count().ToString();
            TopChiefsCountLabel.Content = data.Managers.Where(manager => manager.IdChief == null).Count().ToString();
            SubordinatesCountLabel.Content = data.Managers.Where(manager => manager.IdChief != null).Count().ToString();
            ITDepartmentCountLabel.Content = data.Managers.Where(manager => manager.IdMainDep == new Guid("d3c376e4-bce3-4d85-aba4-e3cf49612c94")).Count().ToString();
            TwoDepartmentsCountLabel.Content = data.Managers.Where(manager => manager.IdMainDep != null && manager.IdSecDep != null).Count().ToString();
            data.Departments.Load();
            DepartmentsView = data.Departments.Local.ToObservableCollection();
            departmentsList.ItemsSource = DepartmentsView;
            departmentsListView =
            CollectionViewSource.GetDefaultView(DepartmentsView);
            departmentsListView.Filter =
              item => (item as Data.Entity.Department)?.DeleteDt == null;

        }

        public class Pair
        {
            public String Key { get; set; } = null!;
            public String? Value { get; set; }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Pairs.Clear();
            var Query = data.Managers.Where(m => m.IdMainDep == Guid.Parse("a23f98d3-899f-4d9f-be5d-66038e37809a")).Select(m => new Pair() { Key = m.Surname, Value = $"{m.Name[0]}. {m.Secname[0]}" });
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Pairs.Clear();
            var Query = data.Managers.Join(data.Departments, m => m.IdMainDep, d => d.Id, (m, d) => new Pair() { Key = $"{m.Surname} {m.Name[0]}. {m.Secname[0]}.", Value = d.Name }).Skip(3).Take(10);
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Pairs.Clear();
            var Query = data.Managers.Join(data.Managers, m1 => m1.IdChief, m2 => m2.Id, (m1, m2) => new Pair() { Key = $"{m1.Surname} {m1.Name[0]}. {m1.Secname[0]}.", Value = $"{m2.Surname} {m2.Name[0]}. {m2.Secname[0]}." }).Take(10).ToList().OrderBy(pair => pair.Key);
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Pairs.Clear();
            var Query = data.Managers.Join(data.Managers, m1 => m1.Id, m2 => m2.Id, (m1, m2) => new Pair() { Key = $"{m1.Surname} {m1.Name[0]}. {m1.Secname[0]}.", Value = m2.CreateDt.ToString() }).Take(7).ToList().OrderByDescending(pair => DateTime.Parse(pair.Value));
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            Pairs.Clear();
            var Query = data.Managers.Join(data.Departments, m => m.IdSecDep, d => d.Id, (m, d) => new Pair() { Key = $"{m.Surname} {m.Name[0]}. {m.Secname[0]}.", Value = $"{d.Name}" }).Take(10).ToList().OrderBy(pair => pair.Value);
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }
        #region Generator
        private int _N;
        public int N { get => _N++; set => _N = value; }
        #endregion
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            N = 1;
            var Query = data.Departments.Select(d => new Pair() { Key = (N).ToString(), Value = d.Name });
            Pairs.Clear();
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            N = 1;
            var Query = data
                .Departments
                .OrderBy(d => d.Name)
                .AsEnumerable()
                .Select(d => new Pair() { Key = (N).ToString(), Value = d.Name });
            Pairs.Clear();
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            var Query = data.Departments.GroupJoin(data.Managers, d => d.Id, m => m.IdMainDep, (d, m) => new Pair() { Key = d.Name, Value = m.Count().ToString() });
            Pairs.Clear();
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            var Query = data.Managers.GroupJoin(data.Managers, chef => chef.Id, d => d.IdChief, (chef, d) => new Pair() { Key = $"{chef.Surname} {chef.Name[0]}. {chef.Secname[0]}.", Value = d.Count().ToString() }).Where(p => p.Value != "0");
            Pairs.Clear();
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            var Query = data.Managers.GroupJoin(data.Managers, m1 => m1.Surname, m2 => m2.Surname, (m1, m2) => new Pair() { Key = m1.Surname, Value = m2.Count().ToString() }).Where(p => p.Value != "0" && p.Value != "1").Distinct();
            Pairs.Clear();
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button11_Click(object sender, RoutedEventArgs e)
        {
            var Query = data.Departments.GroupJoin(data.Managers, d => d.Id, m => m.IdSecDep, (d, m) => new Pair() { Key = d.Name, Value = m.Count().ToString() });
            Pairs.Clear();
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button12_Click(object sender, RoutedEventArgs e)
        {
            N = 1;
            var Query = data.Managers
                .GroupBy(m => m.Surname)
                .AsEnumerable()
                .Where(m => m.Count() > 1)
                .Select(m => new Pair() { Key = (N).ToString(), Value = m.Key });
            Pairs.Clear();
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button13_Click(object sender, RoutedEventArgs e)
        {
            var Query = data
                .Managers
                .GroupJoin(data.Managers, chef => chef.Id, d => d.IdChief, (chef, d) => new Pair() { Key = d.Count().ToString(), Value = $"{chef.Surname} {chef.Name[0]}. {chef.Secname[0]}." })
                .ToList()
                .OrderByDescending(pair => int.Parse(pair.Key))
                .Take(3);
            Pairs.Clear();
            foreach (var pair in Query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button14_Click(object sender, RoutedEventArgs e)
        {
            var query = data
                .Managers
                .Select(m => new Pair() { Key = m.Surname, Value = m.MainDep.Name });
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void NavButton1_Click(object sender, RoutedEventArgs e)
        {
            var query = data
                            .Managers
                            .Include(m => m.SecDep)
                            .Select(m => new Pair() { Key = m.Surname, Value = m.SecDep.Name == null ? "--" : m.SecDep.Name }); 
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void NavChefButton_Click(object sender, RoutedEventArgs e)
        {
            var query = data
                            .Managers
                            .Include(m => m.Chef) 
                            .Select(m => new Pair() { Key = $"{m.Surname} {m.Name[0]}. {m.Secname[0]}.", Value = m.Chef.Name == null ? "--" : $"{m.Chef.Surname} {m.Chef.Name[0]}. {m.Chef.Secname[0]}." });
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)
            {
                if (item.Content is Data.Entity.Department department)
                {
                    CrudDepartment dialog = new() { Department = department };
                    if (dialog.ShowDialog() ?? false)
                    {
                        if (dialog.Department != null)
                        {
                            if (dialog.IsDeleted)
                            {
                                var dep = data.Departments.Find(department.Id);
                                department.DeleteDt = DateTime.Now;
                                data.SaveChanges();
                                departmentsListView.Refresh();
                            }
                            else
                            {
                                var dep = data.Departments.Find(department.Id);
                                if (dep != null)
                                {
                                    dep.Name = department.Name;
                                }
                                data.SaveChanges();
                                int index = DepartmentsView.IndexOf(department);
                                DepartmentsView.Remove(department);
                                DepartmentsView.Insert(index, department);
                            }
                        }
                        else
                        {
                            data.Departments.Remove(department);
                            data.SaveChanges();
                            departmentsListView.Refresh();
                        }
                    }
                }
            }
        }

        private void AddDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            Data.Entity.Department newDepartment = new() { Id = new Guid(), Name = null! };
            CrudDepartment dialog = new() { Department = newDepartment };
            if (dialog.ShowDialog() ?? false)
            {
                data.Departments.Add(newDepartment);
                data.SaveChanges();
                departmentsListView.Refresh();
            }
        }

        private void Nav3Button_Click(object sender, RoutedEventArgs e)
        {
            var query = data.Departments.Where(d => d.DeleteDt == null).Select(d => new Pair()
            {
                Key = d.Name,
                Value = d.MainManagers.Count().ToString()
            }).ToList()
                .OrderByDescending(pair => int.Parse(pair.Value));
            Pairs.Clear();
            foreach (var pair in query)
            {
                if (pair.Value == "0")
                {
                    pair.Value = "closed";
                }
                Pairs.Add(pair);
            }
        }

        private void Nav4Button_Click(object sender, RoutedEventArgs e)
        {
            var query = data.Departments.Where(d => d.DeleteDt == null).Select(d => new Pair()
            {
                Key = d.Name,
                Value = d.SecManagers.Count().ToString()
            }).ToList()
                .OrderByDescending(pair => int.Parse(pair.Value));
            Pairs.Clear();
            foreach (var pair in query)
            {
                if (pair.Value == "0")
                {
                    pair.Value = "closed";
                }
                Pairs.Add(pair);
            }
        }

        private void Nav5Button_Click(object sender, RoutedEventArgs e)
        {
            var query = data.Managers.Where(d => d.DeleteDt == null)
                .Select(m => new Pair()
                {
                    Key = $"{m.Surname} {m.Name[0]}. {m.Secname[0]}.",
                    Value = m.Workers.Count().ToString()
                })
                .ToList()
                .OrderByDescending(pair => int.Parse(pair.Value));
            Pairs.Clear();
            foreach (var pair in query)
            {
                if (pair.Value != "0")
                {
                    Pairs.Add(pair);
                }
            }
        }
    }
}