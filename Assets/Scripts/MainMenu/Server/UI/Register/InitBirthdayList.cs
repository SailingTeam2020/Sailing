using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{

    public class InitBirthdayList : MonoBehaviour
    {

        [SerializeField]
        private Dropdown yearList;
        [SerializeField]
        private Dropdown monthList;
        [SerializeField]
        private Dropdown dayList;

        private const int OldestYear = 1900;

        // Start is called before the first frame update
        void Start()
        {

            if (yearList)
            {
                yearList.ClearOptions();

                List<string> list = new List<string>();

                int nowYear = System.DateTime.Now.Year;

                for (int y = nowYear, end = OldestYear; y >= end; y--)
                {
                    list.Add(y.ToString("D2"));
                }

                yearList.AddOptions(list);
                yearList.value = 0;

                yearList.onValueChanged.AddListener(delegate
                {
                    DropdownValueChanged();
                });
            }

            if (monthList)
            {
                monthList.ClearOptions();

                List<string> list = new List<string>();

                for (int m = 1, end = 12; m <= end; m++)
                {
                    list.Add(m.ToString("D2"));
                }

                monthList.AddOptions(list);
                monthList.value = 0;

                monthList.onValueChanged.AddListener(delegate
                {
                    DropdownValueChanged();
                });

            }

            if (dayList)
            {
                dayList.ClearOptions();

                List<string> list = new List<string>();

                for (int d = 1, end = 31; d <= end; d++)
                {
                    list.Add(d.ToString("D2"));
                }

                dayList.AddOptions(list);
                dayList.value = 0;
            }

        }

        //2月のみ特殊なため、0に設定
        private readonly int[] maxDays = new int[] { 31, 0, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        /// <summary>
        /// @brief 月が変更された場合、その月の日付数に変更する
        /// </summary>
        void DropdownValueChanged()
        {

            int year = yearList.value + OldestYear;
            int month = monthList.value;
            int day = dayList.value + 1;
            int maxDay = maxDays[month];

            if (month + 1 == 2)
            {
                //うるう年がどうか判定し、うるう年なら2月を29日までに設定
                maxDay = ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0) ? 29 : 28;

            }

            dayList.ClearOptions();

            List<string> list = new List<string>();

            for (int d = 1, end = maxDay; d <= end; d++)
            {
                list.Add(d.ToString("D2"));
            }

            dayList.AddOptions(list);

            //入力されている日付が最大日数を超えていた場合、修正する
            if (day > maxDay)
            {
                day = maxDay;
            }
            dayList.value = day - 1;

        }

    }

}