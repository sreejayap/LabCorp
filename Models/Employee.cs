using Microsoft.AspNetCore.Mvc;

namespace LCTest.Models
{
    public class Employee
    {
        private float maxWorkDays = 260;
        private float maxVacationDays = 10;
        private float noOfWorkDays = 0;
        private float noOfVacationDays = 0;
        private float noOfVacationDaysAvailed = 0;
        [BindProperty]
        public int ID { get; set; }
        public string? FirstName { get; set; } = String.Empty;
        public string? LastName { get; set; } = String.Empty;

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public float MaxWorkDays
        {
            get { return maxWorkDays; }
            set { maxWorkDays = value; }
        }
        public float MaxVacationDays
        {
            get { return maxVacationDays; }
            set { maxVacationDays = value; }
        }
        public float NoOfVacationDays
        {
            get { return noOfVacationDays; }
            set { noOfVacationDays = value; }
        }
        public float NoOfWorkDays
        {
            get { return noOfWorkDays; }
            set { noOfWorkDays = value; }
        }
        public float NoOfVacationDaysAvailed
        {
            get { return noOfVacationDaysAvailed; }
            set { noOfVacationDaysAvailed = value; }
        }

        public Employee AddDaysWork(Employee objEmployee, float days)
        {
            float currentDays = objEmployee.NoOfWorkDays;
            float currVacDays = objEmployee.NoOfVacationDays;
            float currVacUsedDays = objEmployee.NoOfVacationDaysAvailed;
            float newcurrentDays = days + currentDays;
            float maxDays = objEmployee.MaxWorkDays;
            float maxVacDays = objEmployee.maxVacationDays;
            float vacRatio = (float)Math.Round(maxDays / maxVacDays, 2);
            float newVDays = 0;
            if (currentDays == 0)
            {
                newVDays = (float)Math.Round(newcurrentDays / vacRatio);
            }
            else
            {
                if (currVacDays == 0)
                {
                    newVDays = (float)Math.Round(newcurrentDays / vacRatio);
                    if (newVDays == 0)
                    {
                        if (currVacUsedDays <=maxDays)
                        {

                            newVDays = (float)Math.Round(newcurrentDays / vacRatio);
                        }
                    }
                    else
                    {
                        newVDays = (float)Math.Floor(days / vacRatio);
                        if (newVDays == 0)
                        {

                            newVDays = (float)Math.Round(newcurrentDays / vacRatio);
                            newVDays -=currVacDays;
                        }
                    }
                }
            }
            objEmployee.NoOfVacationDays= newVDays;
            objEmployee.NoOfWorkDays= newcurrentDays;
            return objEmployee;
        }

        public Employee AddVacationTaken(Employee objEmployee, float days)
        {
            float currentDays = objEmployee.NoOfWorkDays;
            float currVacDays = objEmployee.NoOfVacationDays;
            float currVacUsedDays = objEmployee.NoOfVacationDaysAvailed;
            float newcurrentDays = days + currentDays;
            float maxDays = objEmployee.MaxWorkDays;
            float maxVacDays = objEmployee.maxVacationDays;
            float vacRatio = (float)Math.Round(maxDays / maxVacDays, 2);
            float newVDays = 0;
            if (currVacDays == 0)
            {
                newVDays = days;
            }
            else
            {
                if (currVacDays == 0)
                {
                }
                else
                {
                    if (currVacUsedDays == 0)
                    {
                        newVDays = days;
                        currVacDays = currVacDays - days;
                    }
                    else
                    {
                        newVDays = currVacUsedDays + days;
                        currVacDays = currVacDays - days;

                    }
                }
            }
            objEmployee.NoOfVacationDays = currVacDays;
            objEmployee.NoOfVacationDaysAvailed = newVDays ;
            return objEmployee;
        }

    }
}
