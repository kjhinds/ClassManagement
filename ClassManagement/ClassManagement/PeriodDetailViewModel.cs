using System;
using Xamarin.Forms;
using System.Windows.Input;

namespace ClassManagement
{
    class PeriodDetailViewModel : ViewModelBase
    {
        private SortableObservableCollection<Period> periods;
        private Period period;
        private string periodName;
        private TimeSpan dailyStartTime;
        private TimeSpan dailyEndTime;
        private SortableObservableCollection<TimeSpan> customStartTimes;
        private SortableObservableCollection<TimeSpan> customEndTimes;
        private bool customTimes;
        private bool dailyTimes;
        private bool isNewPeriod;
        private bool enableDonebutton;

        public bool EnableDonebutton
        {
            get { return enableDonebutton; }
            set { SetProperty(ref enableDonebutton, value); }
        }

        public string PeriodName
        {
            get { return periodName; }
            set
            {
                SetProperty(ref periodName, value);
                EnableDonebutton = value.Length > 0;
            }
        }

        public TimeSpan DailyStartTime
        {
            get { return dailyStartTime; }
            set
            {
                SetProperty(ref dailyStartTime, value);
                UpdateAllCustomStartTimes(value);
            }
        }

        public TimeSpan DailyEndTime
        {
            get { return dailyEndTime; }
            set
            {
                SetProperty(ref dailyEndTime, value);
                UpdateAllCustomEndTimes(value);
            }
        }

        public SortableObservableCollection<TimeSpan> CustomStartTimes
        {
            get { return customStartTimes; }
            set { SetProperty(ref customStartTimes, value); }
        }

        public SortableObservableCollection<TimeSpan> CustomEndTimes
        {
            get { return customEndTimes; }
            set { SetProperty(ref customEndTimes, value); }
        }

        public bool CustomTimes
        {
            get { return customTimes; }
            set { SetProperty(ref customTimes, value); }
        }

        public bool DailyTimes
        {
            get { return dailyTimes; }
            set { SetProperty(ref dailyTimes, value); }
        }

        public PeriodDetailViewModel(SortableObservableCollection<Period> periods, Period period)
        {
            this.periods = periods;

            if (period == null)
            {
                isNewPeriod = true;
                this.period = new Period();
            }
            else
            {
                isNewPeriod = false;
                this.period = period;
            }

            PeriodName = this.period.PeriodName;
            CustomTimes = this.period.CustomTimes;
            DailyTimes = !CustomTimes;
            DailyStartTime = this.period.DailyStartTime;
            DailyEndTime = this.period.DailyEndTime;
            CustomStartTimes = this.period.CustomStartTimes;
            CustomEndTimes = this.period.CustomEndTimes;


            DoneButton = new Command(
                (o) => OnDoneButtonClicked());
            CancelButton = new Command(
                (o) => OnCancelButtonClicked());
        }

        public ICommand DoneButton { protected set; get; }

        private void OnDoneButtonClicked()
        {
            period.PeriodName = PeriodName;
            period.CustomTimes = CustomTimes;
            period.DailyStartTime = DailyStartTime;
            period.DailyEndTime = DailyEndTime;
            period.CustomStartTimes = CustomStartTimes;
            period.CustomEndTimes = CustomEndTimes;
            if (isNewPeriod)
            {
                foreach (var item in periods)
                {
                    if (item.PeriodName == PeriodName)
                    {
                        MessagingCenter.Send(this, "Duplicate Period");
                        return;
                    }
                }
                periods.Add(period);
                
            }
            MessagingCenter.Send(this, "Close Period Detail Page");
        }

        private bool EnableButton()
        {
            return this.PeriodName.Length > 0;
        }

        public ICommand CancelButton { protected set; get; }

        private void OnCancelButtonClicked()
        {
            MessagingCenter.Send(this, "Close Period Detail Page");
        }

        private void UpdateAllCustomStartTimes(TimeSpan time)
        {
            if (!CustomTimes && CustomStartTimes != null)
            {
                SortableObservableCollection<TimeSpan> startTimes = new SortableObservableCollection<TimeSpan>();
                foreach (var startTime in CustomStartTimes)
                {
                    startTimes.Add(time);
                }
                CustomStartTimes = startTimes;
            }

        }

        private void UpdateAllCustomEndTimes(TimeSpan time)
        {
            if (!CustomTimes && CustomStartTimes != null)
            {
                SortableObservableCollection<TimeSpan> endTimes = new SortableObservableCollection<TimeSpan>();
                foreach (var endTime in CustomEndTimes)
                {
                    endTimes.Add(time);
                }
                CustomEndTimes = endTimes;
            }

        }
    }
}
