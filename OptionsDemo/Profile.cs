﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsDemo
{
    public class Profile:IEquatable<Profile>
    {

        public Gender Gender { get; set; }
        public int Age { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public Profile() { }
        public Profile(Gender gender, int age, string emailAddress, string phoneNo)
        {
            Gender = gender;
            Age = age;
            ContactInfo = new ContactInfo
            {
                EmailAddress = emailAddress,
                PhoneNo = phoneNo
            };
        }
        public bool Equals(Profile other)
        {
            return other == null ? false : Gender == other.Gender && Age == other.Age && ContactInfo.Equals(other.ContactInfo);
        }
    }

    public class ContactInfo : IEquatable<ContactInfo>
    {
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public bool Equals(ContactInfo other) => other == null ? false : EmailAddress == other.EmailAddress && PhoneNo == other.PhoneNo;
    }

    public enum Gender
    {
        Male,
        Female
    }
}
