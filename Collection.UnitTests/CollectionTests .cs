using Collections;

namespace Collection.UnitTests
{
    public class CollectionTests
    {
        private object coll;
        private object collection;

        [Test]    
        public void Test_Collection_EmptyConstructor() 
        {
            // Arangge and Act

            var coll = new Collection<int>();

            // Assert
            Assert.AreEqual(coll.ToString(), "[]");  
            
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem() 
        {
            var coll = new Collection<int>(5);         
            
            var actual = coll.ToString();   
            var expected = "[5]";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems() 
        {
            var coll = new Collection<int>(5, 6);

            // Assert
            Assert.AreEqual(coll.ToString(), "[5, 6]");
        }

        [Test]
        public void Test_Collection_CountAndCapacity()
        {
            var coll = new Collection<int>(5, 6);
            // Assert
            Assert.AreEqual(coll.Count, 2, "Check for Count");
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(coll.Count)); //GreaterThanOrEqualTo �� ������ � �� �����
        }

        [Test]
        public void Test_Collection_Add() 
        {
            //Arrange
            var coll = new Collection<string>("Ivan", "Peter");

            // Act
            coll.Add("Gosho");

            //Assert
            Assert.AreEqual(coll.ToString(), "[Ivan, Peter, Gosho]");
        }
        [Test]

        public void Test_Collection_GetByIndex() 
        {
            var collection  = new Collection<int>(5, 6, 7); // ���������� �� ���� ��������, ����� ��� 3 ��������  ������ 0 (5), ������ 1 (6), ������� 2(7)
            var item = collection [1]; //����� �� �������� ����� � � ������ 1 (6)

            Assert.That(item.ToString(), Is.EqualTo("6")); // � ����������� ���� �� ����� � ��� �� �� ������ get 50 ��� �� ����� Collection.cs
        }

        [Test]

        public void Test_Collection_SetByIndex()
        {
            var collection = new Collection<int>(5, 6, 7);
            collection[1] = 666; 

            Assert.That(collection.ToString(), Is.EqualTo("[5, 666, 7]")); 
        }

        [Test]

        public void TestGetByInvalidIndex()
        {
            //Arrange
            var coll = new Collection<string>("Ivan", "Peter"); //��������� �� �������� ��� 2 ������ �������� ����� � ������ 0 (Ivan) � ������ 1 (Peter)

            Assert.That(() => { var item = coll[2]; }, // ������� ���� item (������ 2) �� ����� ������� ����� ����
                Throws.InstanceOf<ArgumentOutOfRangeException>()); //tuk �� ������ � ArgumentOutOfRangeExceptio
        }

        [Test]
        
        public void Test_AddRangeWithCrow()
        {
            Collection<int> coll = new Collection<int>(1, 2);

            Assert.That(coll.Count, Is.EqualTo(2), "Count");
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(1), "Capacity");

            for (int i = 0; i < 50; i++)
            {
                coll.Add(i);
            }

            Assert.That(coll.Count, Is.EqualTo(52));
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(52));

            var expected = "[1, 2, " + String.Join(", ", Enumerable.Range(0, 50).ToArray()) + "]";

            Assert.AreEqual(coll.ToString(), expected);

        }
        //Data Driven Test
        [TestCase("Peter,Maria,Ivan", 0, "Peter")]
        [TestCase("Peter,Maria,Ivan", 1, "Maria")]
        [TestCase("Peter,Maria,Ivan", 2, "Ivan")]
        [TestCase("Peter", 0, "Peter")]
        public void Test_Collection_GetByValidIndex( string data, int index, string expected) 
        // string data ("Peter,Maria,Ivan"), int index (0), string expected ("Peter") 
        {
            var coll = new Collection<string>(data.Split(",")); 
           //���������� �� �������� ��� ������ ���� ��� ���� ��� ������ ��� ����� �� �� �� ������ ������� �����
            
            var actual = coll[index]; // ������� ���������� �� ������ ����� � 0

            Assert.That(actual, Is.EqualTo(expected));  
        }
        //Data Driven Test
        [TestCase("", 0)]  // � ����  ���� ����� ���� 2 ���������, ��� ���� ������� ��������� ������ ��� �������� ������ Exception
        [TestCase("Peter", -1)]
        [TestCase("Peter,Maria,Steve", -1)]
        [TestCase("Peter,Maria,Steve", 3)]
        [TestCase("Peter,Maria,Steve", 150)]
        public void Test_Collection_GetByInvalidIndex(string data, int index) 
        {
            var items = new Collection<string>(data.Split(",", StringSplitOptions.RemoveEmptyEntries));

            Assert.That(() => items [index],  Throws.TypeOf<ArgumentOutOfRangeException>());
            // � ����� ��������� () => ������� �������� � ����������� ���� ����� � ������
        }
    }
}