using Microsoft.AspNetCore.Mvc;
using TrainingProjectAPI.Models;
using TrainingProjectAPI.Models.DB;
using TrainingProjectAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrainingProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }


        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var customersList = _customerService.GetListCustomer();
                var response = new GeneralResponse
                {
                    StatusCode = "01",
                    StatusDesc = "Success",
                    Data = customersList
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new GeneralResponse
                {
                    StatusCode = "99",
                    StatusDesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(response);
            }
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var CustomerId = _customerService.GetById(id);
            return Ok(CustomerId);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            try
            {
                var insertCustomer = _customerService.CreateCustomer(customer);
                if (insertCustomer)
                {
                    var responseSucces = new GeneralResponse
                    {
                        StatusCode = "01",
                        StatusDesc = "Insert Customer Succes",
                        Data = null
                    };
                    return Ok(responseSucces);
                }

                var responseFailed = new GeneralResponse
                {
                    StatusCode = "02",
                    StatusDesc = "Insert Customer Failed",
                    Data = null
                };
                return BadRequest(responseFailed);
            }
            catch (Exception ex)
            {
                var responseFailed = new GeneralResponse
                {
                    StatusCode = "99",
                    StatusDesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(responseFailed);
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut]
        public IActionResult Put(Customer customer)
        {
            try
            {
                var updateCustomer = _customerService.UpdateCustomer(customer);
                if (updateCustomer)
                {
                    var responseSucces = new GeneralResponse
                    {
                        StatusCode = "01",
                        StatusDesc = "Update Customer Succes",
                        Data = null
                    };
                    return Ok(responseSucces);
                }

                var responseFailed = new GeneralResponse
                {
                    StatusCode = "02",
                    StatusDesc = "Insert Customer Failed",
                    Data = null
                };
                return BadRequest(responseFailed);
            }
            catch (Exception ex)
            {
                var responseFailed = new GeneralResponse
                {
                    StatusCode = "99",
                    StatusDesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(responseFailed);
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleteCustomer = _customerService.DeleteCustomer(id);
                if (deleteCustomer)
                {
                    var responseSucces = new GeneralResponse
                    {
                        StatusCode = "01",
                        StatusDesc = "Delete Customer Succes",
                        Data = null
                    };
                    return Ok(responseSucces);
                }

                var responseFailed = new GeneralResponse
                {
                    StatusCode = "02",
                    StatusDesc = "Data tidak ditemukan",
                    Data = null
                };
                return BadRequest(responseFailed);
            }
            catch (Exception ex)
            {
                var responseFailed = new GeneralResponse
                {
                    StatusCode = "99",
                    StatusDesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(responseFailed);
            }
        }
    }
}