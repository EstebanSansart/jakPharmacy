using Api.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiJakPharmacy.Controllers.Utilities;
[ApiVersion("1.0")]
public class CustomQueriesController : BaseApiController{

    private readonly IUnitOfWork _UnitOfWork;

    private readonly ILogger<AuthController> _Logger;

    private readonly IMapper _Mapper;

    public CustomQueriesController(
            IUnitOfWork unitOfWork,       
            ILogger<AuthController> logger,
            IMapper mapper
    ){
         _Mapper = mapper;
         _Logger = logger;
         _UnitOfWork = unitOfWork;        
    }
    //*1 Obtener todos los medicamentos con menos de 50 unidades en stock
   [HttpGet("MinStock/{minStock}")]
    //[Authorize]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> MinStock(int minStock){
      return await _UnitOfWork.CustomQueries.MedicineWithMinStock(minStock);                                                       
   }
   
   //*Fin de la consulta 1

   //Obtener Fecha Expiración por año
   [HttpGet("ExpireDate/{year}")]
   //[Authorize]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> MedicineExpireBeforeYear(int year){
      return await _UnitOfWork.CustomQueries.MedicineExpireBeforeYear(year);
   }

   [HttpGet("ExpireInDate/{year}")]
   //[Authorize]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> MedicineExpireInYear(int year){
      return await _UnitOfWork.CustomQueries.MedicineExpireInYear(year);
   }

   //*3 Medicamentos comprados al ‘Proveedor A’
   //*11 Número de medicamentos por proveedor.
   //*13 Proveedores que no han vendido medicamentos en el último año.
   [HttpGet("PurchasedByProvider")]
   //[Authorize]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> PurchasedByProvider([FromBody ] MedicineInfoProviderModel data = null){
      return await _UnitOfWork.CustomQueries.PurchasedByProvider(data);
   }
   //* fin de la consulta 
   //*29. Proveedores de los medicamentos con menos de 50 unidades en stock.
   //*38. Medicamentos con un precio mayor a 50 y un stock menor a 100.
  [HttpGet("MedicinesByPriceAndStock")]
   //[Authorize]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> MedicinesByPriceAndStock([FromBody] MedicinesByPriceAndStockModel data = null){
      return await _UnitOfWork.CustomQueries.MedicinesByPriceAndStock(data);
   }
   //* fin de la consultas

   //*32. Empleado que ha vendido la mayor cantidad de medicamentos distintos en 2023. 
   //*35. Proveedores que han suministrado al menos 5 medicamentos diferentes en 2023.
   //falla
   [HttpGet("ProvidersWhoHaveProvidedDifferentMedications")]
   //[Authorize]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> ProvidersWhoHaveProvidedDifferentMedications([FromBody] WhoHaveProvidedDifferentMedicationsModel data = null){
      return await _UnitOfWork.CustomQueries.ProvidersWhoHaveProvidedDifferentMedications(data);
   }
   //* fin de la consultas

   /*
   *28. Número total de proveedores que suministraron medicamentos en 2023.
   *24. Proveedor que ha suministrado más medicamentos en 2023.*/
   //fallas
   [HttpGet("ProvidersWhoHaveProviderMedications")]
   //[Authorize]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> ProvidersWhoHaveProviderMedications([FromBody] ProvidersWhoHaveProviderMedications data = null){
      return await _UnitOfWork.CustomQueries.ProvidersWhoHaveProviderMedications(data);
   }
   //* fin de la consultas
   
   /* 
   *20. Empleados que hayan hecho más de 5 ventas en total.
   *23. Empleados que no han realizado ninguna venta en 2023.
   *27. Empleados con menos de 5 ventas en 2023.
   *37. Empleados que no realizaron ventas en abril de 2023. */
   [HttpGet("EmployeesWhoHaveMadeSales")]
   //[Authorize]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> EmployeesWhoHaveMadeSales([FromBody] EmployeesWhoHaveMadeSalesModel data = null){
      return await _UnitOfWork.CustomQueries.EmployeesWhoHaveMadeSales(data);
   }
   //* fin de la consulta

   
   //*5 Total de ventas del medicamento ‘Paracetamol’
   [HttpGet("TotalDrugSales/{MedicineName}")]
   //[Authorize]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<MedicineDetailTotalModel>> TotalDrugSales(string MedicineName){
      return await _UnitOfWork.CustomQueries.TotalDrugSales(MedicineName);
   }
   //* fin de la consulta

   //*7 Total de medicamentos vendidos por cada proveedor.
   [HttpGet("TotalMedicationsSoldByProvider")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> TotalMedicationsSoldByProvider(){
      return await _UnitOfWork.CustomQueries.TotalMedicationsSoldByProvider();
   }
   //* fin de la consulta
   //*7 Total de medicamentos vendidos por cada proveedor.
   [HttpGet("TotalMedicinesProvider")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> TotalMedicinesProvider(){
      return await _UnitOfWork.CustomQueries.TotalMedicinesProvider();
   }

   //*8 Cantidad total de dinero recaudado por las ventas de medicamentos.
   [HttpGet("MoneyRaisedFromSales/{medicineName?}")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<float> MoneyRaisedFromSales(string medicineName = null){
      return await _UnitOfWork.CustomQueries.MoneyRaisedFromSales(medicineName);
   }
   //* fin de la consulta


   //*9 Medicamentos que no han sido vendidos
   //*34 Medicamentos que no han sido vendidos en 2023.
   //*21 Medicamentos que no han sido vendidos nunca.   
   //falla 
   [HttpGet("MedicationsThatHaveNotBeenSold/{year?}")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> MedicationsThatHaveNotBeenSold(int? year = null){
      return await _UnitOfWork.CustomQueries.MedicationsThatHaveNotBeenSold(year);
   }
   //*16 Ganancia total por proveedor en 2023 (asumiendo un campo precioCompra en Compras).
   [HttpGet("ProfitsPerProvider")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> ProfitsPerProvider(){
      return await _UnitOfWork.CustomQueries.ProfitsPerProvider();
   }
   //* 22. Paciente que ha gastado más dinero en 2023.
   [HttpGet("VipBuyer")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<object> VipBuyer(){
      return await _UnitOfWork.CustomQueries.VipBuyer();
   }
   //*30. Pacientes que no han comprado ningún medicamento en 2023. 
   [HttpGet("VipBuyer/{year}")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> PersonNoPurchasedYear(int year){
      return await _UnitOfWork.CustomQueries.PersonNoPurchasedYear(year);
   }
   //*32. Empleado que ha vendido la mayor cantidad de medicamentos distintos en 2023. 
   [HttpGet("VipBuyer")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> EmployeesWhoHaveProvidedDifferentMedications(){
      return await _UnitOfWork.CustomQueries.EmployeesWhoHaveProvidedDifferentMedications();
   }
   //*33. Total gastado por cada paciente en 2023.
   [HttpGet("VipBuyer/{year}")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<object> AllSaleCustomer(int year){
      return await _UnitOfWork.CustomQueries.AllSaleCustomer(year);
   }
   //*35. Proveedores que han suministrado al menos 5 medicamentos diferentes en 2023.
   /* [HttpGet("ProvidersWhoHaveProvidedDifferentMedications")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<object> ProvidersWhoHaveProvidedDifferentMedications(){
      return await _UnitOfWork.CustomQueries.ProvidersWhoHaveProvidedDifferentMedications();
   }
    */

   //*15 Obtener el medicamento menos vendido en 2023
   //*17 Promedio de medicamentos comprados por venta.
   //*14 Obtener el total de medicamentos vendidos en marzo de 2023.
   //*26 Total de medicamentos vendidos por mes en 2023.
   //*31 Medicamentos que han sido vendidos cada mes del año 2023.
   //*36 Total de medicamentos vendidos en el primer trimestre de 2023.
   [HttpGet("TotalMedicineSold")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> TotalMedicineSold([FromBody] MedicineTotalSoldModel data = null){
      return await _UnitOfWork.CustomQueries.TotalMedicineSold(data);
   }
   //* fin de la consulta

   //*10 Obtener el medicamento más caro   
   [HttpGet("GetTheMostExpensiveMedicine")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<MedicineInfoSimpleDto> GetTheMostExpensiveMedicine(){
      var medicines = await _UnitOfWork.Medicine_Infos.GetAllAsync();
      float maxPrice = medicines.Max(x => x.Price - (x.Price * (x.Discount / 100)));
      var MostExpensiveMedicine = medicines.First(x => x.Price - (x.Price * (x.Discount / 100)) == maxPrice);
      return _Mapper.Map<MedicineInfoSimpleDto>(MostExpensiveMedicine);
   }
   //* fin de la consulta

   //*12 Pacientes que han comprado Paracetamol.
   [HttpGet("PatientsWhoHaveAcquiredParacetamol")]
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IEnumerable<object>> PatientsWhoHaveAcquiredParacetamol([FromQuery] string Patients = null ){
      return await _UnitOfWork.CustomQueries.PatientsWhoHaveAcquiredParacetamol(Patients);
   }
   //*25 Pacientes que compraron el medicamento “Paracetamol” en 2023.
   [HttpGet("SaleMedicine/{medidineInput}/{year}")]
    //[Authorize] 
   [MapToApiVersion("1.0")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<object> GetSaleParacetamol([FromQuery]string medidineInput,int year){
      return await _UnitOfWork.CustomQueries.GetSaleParacetamol(medidineInput,year);
   }


   //* fin de la consulta ..
}
 
