using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GradesTest
{
    [TestClass]
    public class MainTest
    {
        [TestInitialize]
        public void Init()
        {            
            GradesPrototype.Data.DataSource.CreateData();
        }

        [TestMethod]
        public void TestValidGrade()
        {
            GradesPrototype.Data.Grade grade = new GradesPrototype.Data.Grade(1, "01/01/2012", "Math", "A-", "Very good");
            Assert.AreEqual(grade.AssessmentDate, "1/1/2012");
            Assert.AreEqual(grade.SubjectName, "Math");
            Assert.AreEqual(grade.Assessment, "A-");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBadDate()
        {
            // Future date
            GradesPrototype.Data.Grade grade = new GradesPrototype.Data.Grade(1, "1/1/2023", "Math", "A-", "Very good");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDateNotRecognized()
        {
            // Bad format date
            GradesPrototype.Data.Grade grade = new GradesPrototype.Data.Grade(1, "13/13/2012", "Math", "A-", "Very good");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBadAssessment()
        {
            // Outside range
            GradesPrototype.Data.Grade grade = new GradesPrototype.Data.Grade(1, "1/1/2012", "Math", "F-", "Terrible");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBadSubject()
        {
            // Subject that is not on list
            GradesPrototype.Data.Grade grade = new GradesPrototype.Data.Grade(1, "1/1/2012", "French", "B-", "OK");
        }
    }
}
