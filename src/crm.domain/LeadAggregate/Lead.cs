using crm.domain.Enums;
using crm.domain.Interfaces;
using crm.domain.MetaResults;
using crm.domain.ValueObjects;
using Rolfin.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crm.domain.LeadAggregate
{
    public class Lead : Entity<Guid>, IAggregateRoot
    {
        protected Lead() : base(Guid.NewGuid()) { }

        protected Lead(string leadProducts, string phoneNumber, string delivaryAddress, string email) 
            : base (Guid.NewGuid())
        {
            this.LeadProducts = leadProducts;
            this.CatchLead = DateTime.Now;
            this.DelivaryAddress = new Address(delivaryAddress, 0, null, null, 0);
            this.Client = new Customer(null, phoneNumber, email);
            this.notes = new List<Note>();
        }

        public DateTime CloseLeadDate { get; protected set; }
        public DateTime CatchLead { get; init; }
        public string LeadProducts { get; init; }
        public LeadStage LeadStage { get; protected set; }
        public CloseStatus CloseStatus { get; protected set; }
        public Customer Client { get; protected set; }
        public Address DelivaryAddress { get; protected set; }
        public bool IsClosed => LeadStage == LeadStage.Close ? true : false;
        public decimal ProductsValue { get; protected set; }

        //public From CameFrom { get; init; }

        private readonly List<Note> notes = new List<Note>();
        public IReadOnlyList<Note> Notes
            => notes.AsReadOnly();


        public static Lead New(string leadProducts, string phoneNumber, string delivaryAddress, string email)
        {
            return new Lead(leadProducts, phoneNumber, delivaryAddress, email);
        }

        public Result<bool> UpdateClient(Customer customer)
        {
            if (customer is null)
                return Result<bool>.Invalid();

            this.Client.Update(
                customer.Name, 
                customer.PhoneNumber, 
                customer.EmailAddress
                );

            return Result<bool>.Success();
        }

        public Result<bool> AddNote(string newNote)
        {
            if (!string.IsNullOrWhiteSpace(newNote))
            {
                notes.Add(new Note(newNote));
                return Result<bool>.Success(true);
            }
            else
            {
                return Result<bool>.Invalid(false)
                    .With<NoContent>("The note content is null.");
            }
        }

        public Result<bool> DeleteNote(Guid noteId)
        {
            if(notes.Any(x => x.Id == noteId))
            {
                notes.Remove(notes.Where(x => x.Id == noteId).First());
                return Result<bool>.Success(true);
            }
            else
            {
                return Result<bool>
                    .Invalid(false)
                    .With<NoteNotFound>(
                    "A note with this Id wasn't found."
                    );
            }
        }

        public void CloseLead(CloseStatus closeStatus)
        {
            this.LeadStage = LeadStage.Close;
            this.CloseStatus = closeStatus;
            this.CloseLeadDate = DateTime.UtcNow;
        }

        public Result<bool> SetValue(decimal valueOfProducts)
        {
            if(valueOfProducts is not 0.0m)
            {
                this.ProductsValue = valueOfProducts;
                return Result<bool>.Success(true);
            }
            else
            {
                return Result<bool>.Invalid(false).With<InvalidValue>();
            }

        }
    }
}
