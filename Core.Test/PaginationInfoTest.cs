using Core.Pagination;

namespace Core.Test
{
    public class PaginationInfoTest
    {
        [Fact]
        public void PaginationInfo_DefaultValues_ShoudPass()
        {
            // Arrange
            var paginationInfo = new PaginationInfo();

            // Act
            var expectedPageSize = 4;
            var expectedCurrentPageNumber = 0;
            var expectedLastPage = 0;
            var expectedNumberOfItems = 4;
            var expectedFirstElement = 0;

            // Assert
            Assert.Equal(expectedPageSize, paginationInfo.PageSize);
            Assert.Equal(expectedCurrentPageNumber, paginationInfo.CurrentPageNumber);
            Assert.Equal(expectedLastPage, paginationInfo.LastPage);
            Assert.Equal(expectedNumberOfItems, paginationInfo.NumberOfItems);
            Assert.Equal(expectedFirstElement, paginationInfo.GetFirstElementIndex());
        }
        [Fact]
        public void PaginationInfo_GreaterThanMaxPageNumber_ShoudPass()
        {
            // Arrange
            int numberOfItems = 40;
            int pageSize = 30;
            int currentPageNumber = 1;
            var paginationInfo = new PaginationInfo(numberOfItems, pageSize, currentPageNumber);

            // Act
            var expectedPageSize = 20;
            var expectedCurrentPageNumber = 1;
            var expectedLastPage = 1;
            var expectedNumberOfItems = 40;
            var expectedFirstElement = 20;

            // Assert
            Assert.Equal(expectedPageSize, paginationInfo.PageSize);
            Assert.Equal(expectedCurrentPageNumber, paginationInfo.CurrentPageNumber);
            Assert.Equal(expectedLastPage, paginationInfo.LastPage);
            Assert.Equal(expectedNumberOfItems, paginationInfo.NumberOfItems);
            Assert.Equal(expectedFirstElement, paginationInfo.GetFirstElementIndex());
        }
        [Fact]
        public void PaginationInfo_InvalidCurrentPageNumber_ShouldThrowsException()
        {
            // Arrange
            int numberOfItems = 3;
            int pageSize = 1;
            int currentPageNumber = 3;

            //Act

            // Assert
            Assert.Throws<ArgumentException>(() => new PaginationInfo(numberOfItems, pageSize, currentPageNumber));
        }
        [Theory]
        [InlineData(2, 2, 1)]
        [InlineData(40, 2, 5)]
        public void PaginationInfo_ClaculateValidValues(int numberOfItems, int pageSize, int currentPageNumber)
        {
            // Arrange
            var paginationInfo = new PaginationInfo(numberOfItems, pageSize, currentPageNumber);

            // Act
            var expectedPageSize = pageSize;
            var expectedCurrentPageNumber = currentPageNumber;
            var expectedLastPage = (numberOfItems - 1) / pageSize;
            var expectedNumberOfItems = numberOfItems;
            var expectedFirstElement = currentPageNumber * pageSize;

            // Assert
            Assert.Equal(expectedPageSize, paginationInfo.PageSize);
            Assert.Equal(expectedCurrentPageNumber, paginationInfo.CurrentPageNumber);
            Assert.Equal(expectedLastPage, paginationInfo.LastPage);
            Assert.Equal(expectedNumberOfItems, paginationInfo.NumberOfItems);
            Assert.Equal(expectedFirstElement, paginationInfo.GetFirstElementIndex());
        }
    }
}