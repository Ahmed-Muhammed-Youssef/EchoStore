using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Asp.Models;
using Core.Entities;
using Core.Interfaces;
using Core.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Asp.Controllers;

public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public ActionResult<IReadOnlyList<ProductInfo>> Index()
    {
        var paginationInfo = new PaginationInfo(numberOfItems: 13, 13, 0);
        
        var products =  _unitOfWork.ProductRepository.GetProductsInfo(
            filter: p => true,
            orderBy: null,
            paginationInfo: ref paginationInfo
        );
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
