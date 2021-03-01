using BookStore.BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers.Interfaces
{
    public interface ICrudController<TModel>
    {
        Task<IActionResult> Create(TModel model);

        Task<IActionResult> Get(TModel model);

        Task<IActionResult> GetAll(IndexRequestModel model);

        Task<IActionResult> Update(TModel model);

        Task<IActionResult> Remove(TModel model);

    }
}
