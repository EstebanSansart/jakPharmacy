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

public class MedicineInfoController : BaseApiController{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IMapper _Mapper;

    public MedicineInfoController (IUnitOfWork unitOfWork,IMapper mapper){
        _UnitOfWork = unitOfWork;
        _Mapper = mapper;
    }

    [HttpGet]
    //[Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<MedicineInfoDto>> Get(){
       var records = await _UnitOfWork.Medicine_Infos.GetAllAsync();
       return _Mapper.Map<List<MedicineInfoDto>>(records);
    }

    [HttpGet("{id}")]
    [Authorize]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicineInfoFullDto>> Get(int id){
       var record = await _UnitOfWork.Medicine_Infos.GetByIdAsync(id);
       if (record == null){
           return NotFound();
       }
       return _Mapper.Map<MedicineInfoFullDto>(record);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MedicineInfoDto>>> Get11([FromQuery] Params conf){
       var param = new Param(conf);
       var records = await _UnitOfWork.Medicine_Infos.GetAllAsync(param);
       var recordDtos = _Mapper.Map<List<MedicineInfoDto>>(records);
       IPager<MedicineInfoDto> pager = new Pager<MedicineInfoDto>(recordDtos,records?.Count(),param) ;
       return Ok(pager);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicineInfoDto>> Post(MedicineInfoDto recordDto){
       var record = _Mapper.Map<Medicine_info>(recordDto);
       _UnitOfWork.Medicine_Infos.Add(record);
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
    public async Task<ActionResult<MedicineInfoDto>> Put(int id, [FromBody]MedicineInfoDto recordDto){
       if(recordDto == null)
           return NotFound();
       var record = _Mapper.Map<Medicine_info>(recordDto);
       record.Id = id;
       _UnitOfWork.Medicine_Infos.Update(record);
       await _UnitOfWork.SaveChanges();
       return recordDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
       var record = await _UnitOfWork.Medicine_Infos.GetByIdAsync(id);
       if(record == null){
           return NotFound();
       }
       _UnitOfWork.Medicine_Infos.Remove(record);
       await _UnitOfWork.SaveChanges();
       return NoContent();
    }

    [HttpGet]
    [MapToApiVersion("1.2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MedicineInfoDto>>> Get12([FromQuery] Params conf)
    {
        var param = new Param(conf);
        
        var averageMedicinesForSale = await _UnitOfWork.Medicine_Infos.GetPersonsBoughtParacetamol();

        var records = await _UnitOfWork.Medicine_Infos.GetAllAsync(param);
        var recordDtos = _Mapper.Map<List<MedicineInfoDto>>(records);
        IPager<MedicineInfoDto> pager = new Pager<MedicineInfoDto>(recordDtos, records?.Count(), param);

        var response = new
        {
            Pager = pager,
            averageMedicinesForSale = averageMedicinesForSale
        };

        return Ok(response);
    }
}