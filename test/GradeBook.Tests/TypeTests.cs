using System;
using Xunit;

namespace GradeBook.Tests
{

    public delegate string WriteLogDelegate(string logMessage);
    // a delegate looks like you are defining a method, but it is actually defining a new type
    // different from a class, a delegate describes what a method will look like
    // can point to different methods with the same structure but different implementations
    // basically a variable that you can use like a method

    public class TypeTests
    {
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;
            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello"); // can point to multiple methods
            Assert.Equal(3, count);
        }

        string ReturnMessage(string message) // the names don't need to match, the important parts are the paras and return type
        {
            count++;
            return message;
        }

        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }

        [Fact]
        public void StringsBehaveLikeValueTypes() 
        {
            string name = "Scott";
            string upper = MakeUppercase(name);
            Assert.Equal("SCOTT", upper);
        }

        private string MakeUppercase(string name)
        {
            return name.ToUpper();
        }

        [Fact]
        public void ValueTypesAlsoPassByValue()
        {
            var x = GetInt();
            SetInt(ref x);
            Assert.Equal(42,x);
        }

        private void SetInt(ref int x)
        {
            x = 42;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            //act
            var book1 = GetBook("InMemoryBook 1");
            GetBookSetName(ref book1, "InMemoryBook New"); // the ref keyword
            //assert
            Assert.Equal("InMemoryBook New", book1.Name);
        }

        private void GetBookSetName(ref InMemoryBook inMemoryBook, string name) // the ref keyword 
        {
            inMemoryBook = new InMemoryBook(name);
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            //act
            var book1 = GetBook("InMemoryBook 1");
            GetBookSetName(book1, "InMemoryBook New");
            //assert
            Assert.Equal("InMemoryBook 1", book1.Name);
        }

        private void GetBookSetName(InMemoryBook inMemoryBook, string name)
        {
            inMemoryBook = new InMemoryBook(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            //act
            var book1 = GetBook("InMemoryBook 1");
            SetName(book1, "InMemoryBook New");
            //assert
            Assert.Equal("InMemoryBook New", book1.Name);
        }

        private void SetName(InMemoryBook inMemoryBook, string name)
        {
            inMemoryBook.Name = name;
        }

        [Fact]
        public void BookCalculatesAverageGradeTest()
        {
            //act
            var book1 = GetBook("InMemoryBook 1");
            var book2 = GetBook("InMemoryBook 2");

            //assert
            Assert.Equal("InMemoryBook 1", book1.Name);
            Assert.Equal("InMemoryBook 2", book2.Name);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            //act
            var book1 = GetBook("InMemoryBook 1");
            var book2 = book1;

            //assert
            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }
        InMemoryBook GetBook(string name) 
        {
            return new InMemoryBook(name);
        }
    }
}
