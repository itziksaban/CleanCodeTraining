﻿namespace UnitTests.Payments
{
    public class MonthlyArppvoment
    {
        public Guid CompanyId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public List<Decision> Decisions { get; set; }
    }
}
