using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Diagnostics;
using System.Reactive.Linq;

namespace AvaloniaAppFullNameTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // геттер
        public string Greeting => "Welcome to Avalonia!";

        //private readonly ObservableAsPropertyHelper<string> _fullName;
        //public string FullName => _fullName.Value;


        // using ReactiveUI.Fody.Helpers
        [Reactive] public string FirstName { get; set; }
        [Reactive] public string LastName { get; set; }
        [Reactive] public string FullName { get; set; }

        public MainWindowViewModel()
        {
            //this
            //    .WhenAnyValue(vm => vm.FirstName, vm => vm.LastName)
            //    .DistinctUntilChanged()
            //    .Select(t => $"{t.Item1} {t.Item2}")
            //    .Subscribe();

            // позволяют получать уведомления при изменении свойств объектов -> слушатель
            this
                .WhenAnyValue(x => x.FirstName, x => x.LastName)
                // обработать наблюдаемое значение, если с момента преведущего прошло времени
                .Throttle(TimeSpan.FromSeconds(0.6))
                // преобразовать текущее наблюдаемое значение
                .Select(x => $"{x.Item1} {x.Item2}")
                // обработать только уникальные значения
                .DistinctUntilChanged()
                // получать уведомления при каждом изменении значения -> вывод и обработка наблюдаемого значения
                .Subscribe(x =>
                    {
                        Debug.WriteLine($"onNext (FullName): {x}");
                        FullName = x;
                    }
                );

            //.ToProperty(this, vm => vm.FullName);
        }
    }
}
