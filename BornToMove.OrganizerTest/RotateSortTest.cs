using NUnit.Framework;
using Organizer;

namespace BornToMove.OrganizerTest
{
    public class RotateSortTest 
    {
        [Test]
        public void testSortEmpty()
        {

            // prepare
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() {};
            IComparer<int> comparer = Comparer<int>.Default;

            // run
            var result = sorter.Sort(input, comparer);

            // validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(0).Items);
            Assert.That(result, Is.EquivalentTo(new int[] {}));
            Assert.That(input, Is.EquivalentTo(new int[] {}));

        }

        [Test]
        public void testSortOneElement()
        {

            //prepare
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() {2};
            IComparer<int> comparer = Comparer<int>.Default;
            
            //run
            var result = sorter.Sort(input, comparer);

            // validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(1).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { 2 }));
            Assert.That(input, Is.EquivalentTo(new int[] { 2 }));

        }

        [Test]
        public void testSortTwoElements()
        {
            // prepare
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 32, 3 };
            IComparer<int> comparer = Comparer<int>.Default;

            // run
            var result = sorter.Sort(input, comparer);

            // validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(2).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { 3, 32 }));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] { 32, 3 }));
        }

        [Test]
        public void testSortThreeEqual()
        {

            // prepare
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 3, 3, 3 };
            IComparer<int> comparer = Comparer<int>.Default;

            // run
            var result = sorter.Sort(input, comparer);

            // validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(3).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { 3, 3, 3 }));
            // also check that our input is not modified
            Assert.That(input, Is.EquivalentTo(new int[] { 3, 3, 3 }));

        }

        [Test]
        public void testSortUnsortedArrayThreeElements()
        {

            // prepare
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 16, 5, 21 };
            IComparer<int> comparer = Comparer<int>.Default;

            // run
            var result = sorter.Sort(input, comparer);

            // validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(3).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { 5, 16, 21 }));
            Assert.That(input, Is.EquivalentTo(new int[] { 16, 5, 21 }));

        }


        public void testSortUnsortedArraySevenElements()
        {
            // prepare
            RotateSort<int> sorter = new RotateSort<int>();
            List<int> input = new List<int>() { 38, 2, 9, 9, 44, 30, 9 };
            IComparer<int> comparer = Comparer<int>.Default;

            // run
            var result = sorter.Sort(input, comparer);

            // validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(3).Items);
            Assert.That(result, Is.EquivalentTo(new int[] { 2, 9, 9, 9, 30, 38, 44 }));
            Assert.That(input, Is.EquivalentTo(new int[] { 38, 2, 9, 9, 44, 30, 9 }));

        }

    }
}
