using System;

namespace LibrarySystem
{
   
    public class Person
    {
        public string PersonId { get; }
        public string FullName { get; }
        public string Phone { get; }

        protected Person(string personId, string fullName, string phone)
        {
            if (string.IsNullOrWhiteSpace(personId))
            {
                throw new ArgumentException("Person ID must not be null or empty.", nameof(personId));
            }
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Full name must not be null or empty.", nameof(fullName));
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Phone must not be null or empty.", nameof(phone));
            }

            PersonId = personId;
            FullName = fullName;
            Phone = phone;
        }
    }
}
