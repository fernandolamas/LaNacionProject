# La Nacion Project

Un proyecto hecho para el challenge técnico del equipo de La Nación.

En la solución en .NET 6.0 C# encontrará:
1. Un proyecto API con un controlador, un servicio y modelos.
2. Un proyecto que contiene tests unitarios sobre el controlador y el servicios.

## Indice: 

[Pre-requisitos](#pre-requisitos)<br>
[Proyecto API (LaNacionProject)](#proyecto-api-lanacionproject)<br>
[Puesta en marcha del proyecto API](#puesta-en-marcha-del-proyecto-api)<br>
[Proyecto Tests Unitarios (LaNacionProjectTest)](#proyecto-tests-unitarios-lanacionprojecttest)<br>
[Problemas conocidos sobre el proyecto API (LaNacionProject)](#problemas-conocidos-sobre-el-proyecto-api-lanacionproject)<br>
[Aclaraciones](#aclaraciones)

## Pre-requisitos

Para ejecutar el proyecto ud. necesitará:

1. Visual studio 2022
2. Sql Server

## Proyecto API (LaNacionProject)
La motivación de este proyecto es poner en prueba nuestras capacidades para realizar un ABM (Alta, baja y modificación) sobre un modelo de contacto, en el que cada contacto este compuesto por:
- Un nombre
- Una compañia
- Una imagen de perfil (Una url en el que disponga la imagen)
- Un email
- Una fecha de cumpleaños
- Una lista de números de teléfono (Cada teléfono tiene un tipo que puede ser work, personal, o lo que se desee)
- Una dirección compuesta por state y city

Ud. contará con Swagger para probar los endpoints del controlador.

## Puesta en marcha del proyecto API

A continuación, una secuencia de pasos que le permitirá instalar el proyecto.

1. Abra el proyecto en Visual studio
2. Abra el Package Manager Console
3. En la consola escriba "update-database" sin las comillas, luego presione enter.
4. Si no hubo ningún error, ud. está listo para correr el proyecto

## Proyecto Tests Unitarios (LaNacionProjectTest)

Debido a que es requerimiento del challenge poder realizar un test sobre almenos un endpoint, deberá ejecutar el proyecto sin debuguear, es importante que elija la opción sin debuguear por que de esta forma Visual Studio le permitirá realizar tests en simultaneo con la aplicación encendida.


Para correr los test unitarios:
1. Aseguresé de que el proyecto corre en el puerto establecido en la linea 23 de la clase ContactControllerTests.cs, de lo contrario, modifique el puerto para que apunte al que la ejecución le provea
2. Ejecute el proyecto LaNacionProject sin debuguear
3. En Visual Studio dirijase a la solapa Test -> Test Explorer

Se abrira una nueva ventana en donde Ud. puede probar los tests

## Problemas conocidos sobre el proyecto API (LaNacionProject)

- Los modelos no tienen DTOS [consulte el tutorial de API en .Net para mas información](https://go.microsoft.com/fwlink/?linkid=2123754)
- En contact, la imagen de perfil puede ser cualquier url que no necesariamente sea una imagen (extensión terminada en .jpg, .png, etc...)
- Al realizar un PUT sobre contact, si se desea añadir un nuevo teléfono, Entity Framework lanzará una excepción

## Aclaraciones

- No es posible tener contacts con emails repetidos, o teléfonos repetidos en el sistema
- Los tests no usan Moq
