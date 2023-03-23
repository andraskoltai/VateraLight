using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VateraLight.Application.Interfaces;
using VateraLight.Domain;

namespace VateraLight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationLogic _reservationLogic;

        public ReservationController(IReservationLogic reservationLogic)
        {
            _reservationLogic = reservationLogic;
        }

        [HttpGet]
        public async Task<IActionResult> Reserve(string userName, int count)
        {
            var reservations = _reservationLogic.ReserveProducts(new User(userName), count);
            if (reservations.Count() == 0)
            {
                // why did it fail (no stock/wrong username/invalid count)
                return BadRequest();
            }

            foreach (var reservation in reservations)
            {
                Console.WriteLine(reservation.ToString() + " has been added to the processing queue. " + DateTime.Now);
            }
            
            return Ok(reservations);
        }
    }
}
