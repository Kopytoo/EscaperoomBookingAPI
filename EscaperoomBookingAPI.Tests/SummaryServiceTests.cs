using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using EscaperoomBookingAPI.Core.Application.Context.Interface;
using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;
using EscaperoomBookingAPI.Core.Application.Services;
using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;
using EscaperoomBookingAPI.Core.Domain.Services.Interfaces;
using EscaperoomBookingAPI.Infrastructure.Persistence.Context;
using EscaperoomBookingAPI.Infrastructure.Repositories.Master;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EscaperoomBookingAPI.Tests;

[TestFixture]
public class SummaryServiceTests
{
    private IFixture _fixture;
    private ISummaryService _summaryService;
    private Mock<IUnitOfWork> _unitOfWorkMock;

    public SummaryServiceTests()
    {
        _fixture = null;
        _summaryService = null;
        _unitOfWorkMock = null;
    }

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _summaryService = new SummaryService(_unitOfWorkMock.Object);
    }

    [Test]
    [TestCase("2022-09-12")]
    [TestCase("2022-09-13")]
    [TestCase("2022-09-14")]
    [TestCase("2022-09-15")]
    public async Task UpdateSummaryAsync_WhenBookingDetailsVisitDateDayOfWeekIsWeekday_ReturnsSummaryWithVariantAndPriceExpected(DateTime dateTime)
    {
        //Arrange
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        _fixture.Customize<BookingDetails>(c => c.With(p => p.VisitDate, dateTime));

        var summary = _fixture.Create<Summary>();
        var bookingDetails = _fixture.Create<BookingDetails>();
        var customerDetails = _fixture.Create<CustomerDetails>();

        _unitOfWorkMock.Setup(c => c.Summaries.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(summary);
        _unitOfWorkMock.Setup(c => c.BookingsDetails.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(bookingDetails);
        _unitOfWorkMock.Setup(c => c.CustomersDetails.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(customerDetails);
        
        //Act
        var result = await _summaryService.UpdateSummaryAsync(summary.Id, bookingDetails.Id, Guid.Empty);

        //Assert
        result.BookingVariant.Should().Be(Variant.Weekday);
        result.Price.Should().Be(170);
    }
    
    [Test]
    [TestCase("2022-09-16")]
    [TestCase("2022-09-17")]
    [TestCase("2022-09-18")]
    public async Task UpdateSummaryAsync_WhenBookingDetailsVisitDateDayOfWeekIsWeekend_ReturnsSummaryWithVariantAndPriceExpected(DateTime dateTime)
    {
        //Arrange
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        _fixture.Customize<BookingDetails>(c => c.With(p => p.VisitDate, dateTime));

        var summary = _fixture.Create<Summary>();
        var bookingDetails = _fixture.Create<BookingDetails>();
        var customerDetails = _fixture.Create<CustomerDetails>();

        _unitOfWorkMock.Setup(c => c.Summaries.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(summary);
        _unitOfWorkMock.Setup(c => c.BookingsDetails.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(bookingDetails);
        _unitOfWorkMock.Setup(c => c.CustomersDetails.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(customerDetails);
        
        //Act
        var result = await _summaryService.UpdateSummaryAsync(summary.Id, bookingDetails.Id, Guid.Empty);

        //Assert
        result.BookingVariant.Should().Be(Variant.Weekend);
        result.Price.Should().Be(200);
    }
}