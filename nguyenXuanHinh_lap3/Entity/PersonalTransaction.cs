using System;

namespace nguyenXuanHinh_lap3.Entity
{
    class PersonalTransaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Money { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Category { get; set; }

        public PersonalTransaction(string name, string description, double money, DateTime createDate, int category)
        {
            Name = name;
            Description = description;
            Money = money;
            CreatedDate = createDate;
            Category = category;
        }

        public PersonalTransaction(){}
    }
}
