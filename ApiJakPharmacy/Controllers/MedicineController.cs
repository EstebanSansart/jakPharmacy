using ApiJakPharmacy.Controllers;
using ApiJakPharmacy.Dtos;
using AutoMapper;
using Domain.Entities;
using ApiJakPharmacy.Helpers;
using Domain.Interfaces.Params;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiJakPharmacy.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
[ApiVersion("1.2")]
[ApiVersion("1.3")]
[ApiVersion("1.4")]

public class MedicineController : BaseApiController{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IMapper _Mapper;

    public MedicineController (IUnitOfWork unitOfWork,IMapper mapper){
        _UnitOfWork = unitOfWork;
        _Mapper = mapper;
    }

    [HttpGet]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<MedicineDto>> Get(){
       var records = await _UnitOfWork.Medicines.GetAllAsync();
       return _Mapper.Map<List<MedicineDto>>(records);
    }


    [HttpGet("ProviderContact")]
    //[Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<object>> ProviderContact(){
       return await _UnitOfWork.Medicines.GetProviderMedicineContact();                                                       
    }



    [HttpGet("{id}")]
    [Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicineFullDto>> Get(int id){
       var record = await _UnitOfWork.Medicines.GetByIdAsync(id);
       if (record == null){
           return NotFound();
       }
       return _Mapper.Map<MedicineFullDto>(record);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MedicineDto>>> Get11([FromQuery] Params conf){
       var param = new Param(conf);
       var records = await _UnitOfWork.Medicines.GetAllAsync(param);
       var recordDtos = _Mapper.Map<List<MedicineDto>>(records);
       IPager<MedicineDto> pager = new Pager<MedicineDto>(recordDtos,records?.Count(),param) ;
       return Ok(pager);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicineDto>> Post(MedicineDto recordDto){
        var record = _Mapper.Map<Medicine>(recordDto);
        _UnitOfWork.Medicines.Add(record);
        await _UnitOfWork.SaveChanges();
        if (record == null){
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post),new {id= record.Id, recordDto});
    }

    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicineDto>> Put(int id, [FromBody]MedicineDto recordDto){
       if(recordDto == null)
           return NotFound();
       var record = _Mapper.Map<Medicine>(recordDto);
       record.Id = id;
       _UnitOfWork.Medicines.Update(record);
       await _UnitOfWork.SaveChanges();
       return recordDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
       var record = await _UnitOfWork.Medicines.GetByIdAsync(id);
       if(record == null){
           return NotFound();
       }
       _UnitOfWork.Medicines.Remove(record);
       await _UnitOfWork.SaveChanges();
       return NoContent();

    }



    [HttpGet]
    [MapToApiVersion("1.2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MedicineDto>>> Get12([FromQuery] Params conf)
    {
        var param = new Param(conf);
        var records = await _UnitOfWork.Medicines.GetAllAsync(param);
        var recordDtos = _Mapper.Map<List<MedicineDto>>(records);
        IPager<MedicineDto> pager = new Pager<MedicineDto>(recordDtos, records?.Count(), param);

        var expiracionmedicina = await _UnitOfWork.Medicines.GetMedicinesExpiringBefore2024();

        var response = new
        {
            Pager = pager,
            expiracion = expiracionmedicina
        };

        return Ok(response);
    }
    
    [HttpGet]
    [MapToApiVersion("1.3")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MedicineDto>>> Get13([FromQuery] Params conf)
        {
            var param = new Param(conf);
            var records = await _UnitOfWork.Medicines.GetAllAsync(param);
            var recordDtos = _Mapper.Map<List<MedicineDto>>(records);
            IPager<MedicineDto> pager = new Pager<MedicineDto>(recordDtos, records?.Count(), param);

            var providerMedicineContact = await _UnitOfWork.Medicines.GetProviderMedicineContact();

            var response = new
            {
                Pager = pager,
                expiracion = providerMedicineContact
            };

            return Ok(response);
        }



[HttpGet]
[MapToApiVersion("1.4")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<Medicine>>> GetProviderA()
{
    try
    {
        var medicamentosProveedorA = await _UnitOfWork.Medicines.GetProviderA();
        if (medicamentosProveedorA != null && medicamentosProveedorA.Any())
        {
            return Ok(medicamentosProveedorA);
        }
        else
        {
            return NotFound("No se encontraron medicamentos para el Proveedor A.");
        }
    }
    catch (Exception)
    {
        return BadRequest("Ocurrió un error al obtener los medicamentos del Proveedor A.");
    }
}





    }

