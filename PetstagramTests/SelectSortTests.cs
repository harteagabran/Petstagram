using Petstagram.Models;
using Petstagram.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetstagramTests
{
    public class SelectSortTests
    {
        [Fact]
        public void SelectionSort_By_Date()
        {
            // Arrange
            var service = new StructureService();
            List<Picture> pictureList = new List<Picture>
            {
                new Picture
                {
                    UploadDate = DateTime.Parse("2020-01-01")
                },
                new Picture
                {
                    UploadDate = DateTime.Parse("2024-09-10")
                },
                new Picture
                {
                    UploadDate = DateTime.Parse("2021-12-31")
                },
                new Picture
                {
                    UploadDate = DateTime.Parse("2022-09-01")
                },
                new Picture
                {
                    UploadDate = DateTime.Parse("2000-09-10")
                }
            };

            // Act
            var sortedList = service.SelectionSortByDate(pictureList);

            //Assert
            Assert.Equal(DateTime.Parse("2000-09-10"), sortedList[0].UploadDate);
            Assert.Equal(DateTime.Parse("2020-01-01"), sortedList[1].UploadDate);
            Assert.Equal(DateTime.Parse("2021-12-31"), sortedList[2].UploadDate);
            Assert.Equal(DateTime.Parse("2022-09-01"), sortedList[3].UploadDate);
            Assert.Equal(DateTime.Parse("2024-09-10"), sortedList[4].UploadDate);
        }
    }
}
