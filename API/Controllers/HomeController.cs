using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Asp.Models;
using Core.Entities;
using Core.Interfaces;
using Core.Pagination;
using Microsoft.AspNetCore.Mvc;

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
    // GET: home/details/{id}
    public async Task<ActionResult<ProductInfo>> Details(int id)
    {
        var pi = await _unitOfWork.ProductRepository.GetProductInfoByIdAsync(id);
        return View(pi);
    }

    // GET home/
    public ActionResult<IReadOnlyList<ProductInfo>> Filter([FromQuery]int? productBrandId,
        [FromQuery]int? productTypeId, [FromQuery] string search = "")
    {
        var paginationInfo = new PaginationInfo(numberOfItems: 13, 13, 0);
        // filter construction
        Func<ProductInfo, bool> filter1 = p => true;
        Func<ProductInfo, bool> filter2 = p => true;
        Func<ProductInfo, bool> filter3 = p => true;
        if(productBrandId != null)
        {
            filter1 = p => p.ProductBrandId == productBrandId;
        }
        if(productTypeId != null)
        {
            filter2 = p => p.ProductTypeId == productTypeId;
        }
        if(!string.IsNullOrEmpty(search))
        {
            filter3 = p => p.Name.Contains(search);
        }
        var products = _unitOfWork.ProductRepository.GetProductsInfo(
            filter: p => filter1(p) && filter2(p) && filter3(p),
            orderBy: null,
            paginationInfo: ref paginationInfo
        );
        return View("Index", products);
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
