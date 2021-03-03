using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers.Interfaces
{
    public interface ICrudController<TModel, TCreateModel>
    {
        Task<IActionResult> Create(TCreateModel model);

        Task<IActionResult> Get(BaseModel model);

        Task<IActionResult> GetAll(IndexRequestModel model);

        Task<IActionResult> Update(TModel model);

        Task<IActionResult> Remove(BaseModel model);

    }
}
