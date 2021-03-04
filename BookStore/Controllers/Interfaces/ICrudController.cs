using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.PresentationLayer.Controllers.Interfaces
{
    public interface ICrudController<TModel, TCreateModel>
    {
        Task<IActionResult> Create(TCreateModel model);

        Task<IActionResult> Get(long id);

        Task<IActionResult> GetAll(IndexRequestModel model);

        Task<IActionResult> Update(long id, TModel model);

        Task<IActionResult> Delete(long id);

    }
}
