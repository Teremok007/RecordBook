using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RecordBook.Models
{
    public class Record : IValidatableObject
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Message))
                yield return new ValidationResult("Message cant be empty");

            if (string.IsNullOrWhiteSpace(Author))
                yield return new ValidationResult("Author cant be empty");

            if (Author.Length < 3)
                yield return new ValidationResult("Length Author must be more 2 letters");
        }
    }
}