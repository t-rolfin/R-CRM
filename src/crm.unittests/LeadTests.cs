using AutoFixture;
using crm.domain.LeadAggregate;
using crm.domain.MetaResults;
using FluentAssertions;
using Rolfin.Result.MetaResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace crm.unittests
{
    public class LeadTests
    {
        private Lead _sut;

        public LeadTests()
        {
            _sut = Lead.New(
                "product1", 
                "0761832232", 
                "Bucharest", 
                "rolfin.test@gmail.com"
                );

            _sut.AddNote("Fake note.");
        }

        [Fact]
        public void Lead_Creation()
        {
            
            var lead = Lead.New("", "", "", "");

            lead.Should().NotBeNull();
        }

        [Fact]
        public void AddNote_ShouldAddNewNote()
        {
            int noteNumber = _sut.Notes.Count + 1;
            _sut.AddNote("This is my new note.");

            _sut.Notes.Count.Should().Be(noteNumber);
        }

        [Fact]
        public void AddNote_ShouldReturnTrue_WhenTheNoteContentIsNotNull()
        {
            var result = _sut.AddNote("new note");

            result.MetaResult.Should().BeOfType<Ok>();
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void AddNote_ShouldReturnNoContentResult_WhenTheNoteIsWhitespaceOrNull()
        {
            var result = _sut.AddNote("");

            result.MetaResult.Should().BeOfType<NoContent>();
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void RemoveNote_ShouldRemoveAnExistingNoteById()
        {
            int noteNumber = 0;
            var noteId = _sut.Notes.First().Id;

            _sut.DeleteNote(noteId);

            _sut.Notes.Count.Should().Be(noteNumber);
        }

        [Fact]
        public void RemoveNote_ShouldReturnTrue_WhenNoteIsRemovedWithSuccess()
        {
            Guid noteId = _sut.Notes.First().Id;

            var result = _sut.DeleteNote(noteId);

            result.Value.Should().BeTrue();
            result.MetaResult.Should().BeOfType<Ok>();
        }

        [Fact]
        public void RemoveNote_ShouldReturnFalse_WhenNoteDoesNotExist()
        {
            var fakeId = Guid.NewGuid();

            var result = _sut.DeleteNote(fakeId);

            result.IsSuccess.Should().BeFalse();
            result.MetaResult.Should().BeOfType<NoteNotFound>();
        }

        [Fact]
        public void SetValue_ShouldReturnTrue_WhenValueIsNotDefault()
        {
            var productsValue = 3.3m;

            var result = _sut.SetValue(productsValue);

            result.IsSuccess.Should().BeTrue();
            result.MetaResult.Should().BeOfType<Ok>();
            _sut.ProductsValue.Should().Be(productsValue);
        }

        [Fact]
        public void SetValue_ShouldReturnFalse_WhenValueIsDefault()
        {
            decimal productsValue = default;

            var result = _sut.SetValue(productsValue);

            result.IsSuccess.Should().BeFalse();
            result.MetaResult.Should().BeOfType<InvalidValue>();
            _sut.ProductsValue.Should().Be(productsValue);
        }
    }
}
