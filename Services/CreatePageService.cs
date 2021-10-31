using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Nufi.kyb.v2.Models;
using Nufi.kyb.v2.Services;


namespace Nufi.kyb.v2.Services
{
    public class CreatePageService
    {
        public NufiApiService ApiService;
        public IWebHostEnvironment WebHostEnvironment { get; }

        public CreatePageService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public InformPage CreateInformPage(
            ActaConstitutiva actaConstitutiva, 
            SATRequest sat, IMPIRequest impi, 
            string razon_social, string rfc, string marca, 
            AntecedentesPMNRequest antecedentes)
        {
            InformPage generalPage = new InformPage(
                CreateGeneralSections(actaConstitutiva, sat, impi, rfc, marca),
                CreateAntecedentesSections(antecedentes, razon_social),
                CreateRepresentantesSections(actaConstitutiva.representantes_legales),
                CreateSociosFisicosSections(actaConstitutiva.socios_fisicos),
                CreateSociosMoralesSections(actaConstitutiva.socios_morales),
                new SuperSeccion[]{});
            return generalPage;
        }
        
        public SuperSeccion[] CreateGeneralSections(
            ActaConstitutiva actaConstitutiva, 
            SATRequest sat, 
            IMPIRequest impi, 
            string rfc, string marca)
        {
            List<Seccion> seccionesSatLista = new List<Seccion>();
            string tituloSeccionSat = "Servicio de Administración Tributaria (SAT)";
            SuperSeccion superSeccionSat, superSeccionImpi;
            List<Seccion> seccionesImpiLista = new List<Seccion>();
            string tituloSeccionImpi = "Instituto Mexicano de la Propiedad Industrial (IMPI)";
            int i = 0;

            var datosGenerales = new Dato[]
            {
                new Dato("Razón Social", actaConstitutiva.razon_social),
                new Dato("Marca", actaConstitutiva.marca),
                new Dato("Nacionalidad", actaConstitutiva.nacionalidad),
                new Dato("Registro Federal de Contribuyentes (RFC)", actaConstitutiva.rfc),
                new Dato("Fecha de Constitución", actaConstitutiva.fecha_constitucion),
                new Dato("Número de Escritura Constitutiva", actaConstitutiva.numero_escritura.ToString()),
                new Dato("Folio Mercantil", actaConstitutiva.folio_mercantil.ToString()),
                new Dato("Fecha de inscripción de PRP", actaConstitutiva.fecha_inscripcion_prp),
                new Dato("Sector", actaConstitutiva.sector),
                new Dato("Giro Mercantíl", actaConstitutiva.giro_mercantil),
                new Dato("Objeto Social", actaConstitutiva.objeto_social)
            };
            var datosDomiciliarios = new Dato[]
            {
                new Dato("Calle", actaConstitutiva.domicilio_fiscal.calle),
                new Dato("Número Exterior", actaConstitutiva.domicilio_fiscal.num_ext.ToString()),
                new Dato("Número Interior", actaConstitutiva.domicilio_fiscal.num_int.ToString()),
                new Dato("Entre Calles", actaConstitutiva.domicilio_fiscal.entre_calles),
                new Dato("Colonia", actaConstitutiva.domicilio_fiscal.colonia),
                new Dato("Código Postal", actaConstitutiva.domicilio_fiscal.codigo_postal.ToString()),
                new Dato("Entidad Federativa / Demarcación", actaConstitutiva.domicilio_fiscal.entidad_federativa),
                new Dato("País", actaConstitutiva.domicilio_fiscal.pais),
                new Dato("Delegación / Municipio", actaConstitutiva.domicilio_fiscal.municipio)
            };
            // SAT API
            if (sat.data is not null)
            {
                foreach (var registro in sat.data)
                {
                    i++;
                    var datosSat = new Dato[]
                    {
                        new Dato("Registro Federal de Contribuyentes (RFC)", registro.rfc),
                        new Dato("Número y Fecha de Oficio Global Presuntos", registro.fecha_oficio_global_presuncion),
                        new Dato("Publicación Página SAT Presuntos", registro.fecha_publi_sat_presuntos),
                        new Dato("Publicación Diario Oficial de la Federación Presuntos", registro.fecha_publi_dof_presuntos),
                        new Dato("Número y Fecha de Oficio Global Desvirtuados", registro.fecha_oficio_global_desvirtuaron),
                        new Dato("Publicación Página SAT Desvirtuados", registro.fecha_publi_sat_desvirtuados),
                        new Dato("Publicación Diario Oficial de la Federación Desvirtuados", registro.fecha_publi_dof_desvirtuaron),
                        new Dato("Número y Fecha de Oficio Global Definitivos", registro.fecha_oficio_global_definitivos),
                        new Dato("Publicación Página SAT Definitivos", registro.fecha_publi_sat_definitivos),
                        new Dato("Publicación Diario Oficial de la Federación Definitivos", registro.fecha_publi_dof_definitivos),
                        new Dato("Número y Fecha de Oficio Global Favorables", registro.fecha_oficio_global_favorable),
                        new Dato("Publicación Página SAT Favorables", registro.fecha_publi_sat_favorable),
                        new Dato("Publicación Diario Oficial de la Federación Favorables", registro.fecha_publi_dof_favorable)
                    };
                    seccionesSatLista.Add(new Seccion("Registro " + i.ToString(), datosSat));
                }
                Seccion[] seccionesSat = seccionesSatLista.ToArray();
                superSeccionSat = new SuperSeccion(true,
                                                   tituloSeccionSat,
                                                   seccionesSat,
                                                   null);
            }
            else
            {
                var datosSat = new Dato[]
                {
                    new Dato("Registro Federal de Contribuyentes (RFC)", rfc),
                    new Dato("Resultado", sat.message)
                };
                superSeccionSat = new SuperSeccion(false,
                                                   tituloSeccionSat,
                                                   null,
                                                   datosSat);
            }
            if (impi.data.Length != 0)
            {
                i = 0;
                foreach (var registro in impi.data)
                {
                    i++;

                    var datosImpi = new Dato[]
                    {
                        new Dato("Nombre", registro.name),
                        new Dato("Numero de Archivo", registro.fileNumber),
                        new Dato("Tipo de Peticion", registro.requestType),
                        new Dato("Cabecera", registro.headline),
                        new Dato("Estado", registro.status),
                    };
                    seccionesImpiLista.Add(new Seccion("Registro " + i.ToString() +
                                                       " - " + registro.name + " - Número de archivo: " +
                                                       registro.fileNumber, datosImpi));
                }
                Seccion[] seccionesImpi = seccionesImpiLista.ToArray();
                superSeccionImpi = new SuperSeccion(true, tituloSeccionImpi, seccionesImpi, null);
            }
            else
            {
                var datosImpi = new Dato[]
                {
                    new Dato("Marca", marca),
                    new Dato("Resultado", impi.message)
                };
                superSeccionImpi = new SuperSeccion(false,
                                                    tituloSeccionImpi,
                                                    null,
                                                    datosImpi);
            }

            SuperSeccion[] superSecciones = new SuperSeccion[]
            {
                new SuperSeccion(false, "Información General", null, datosGenerales),
                new SuperSeccion(false, "Información Domiciliaria", null, datosDomiciliarios),
                superSeccionSat,
                superSeccionImpi
            };
            return superSecciones;
        }

        public SuperSeccion[] CreateAntecedentesSections(AntecedentesPMNRequest antecedentes, string razon_social)
        {
            List<SuperSeccion> superSecciones = new List<SuperSeccion>();

            if (antecedentes.data is not null)
            {
                foreach (var registro in antecedentes.data.resultados)
                {
                    List<Seccion> seccionesLista = new List<Seccion>();
                    foreach (var expediente in registro.expedientes)
                    {
                        var datos = new Dato[]
                        {
                            new Dato("Actor", expediente.actor),
                            new Dato("Demandado", expediente.demandado),
                            new Dato("Fecha", expediente.fecha),
                            new Dato("Fuero", expediente.fuero),
                            new Dato("Juzgado", expediente.juzgado),
                            new Dato("Tipo", expediente.tipo)
                        };
                        seccionesLista.Add(new Seccion("Expediente: " + expediente.expediente, datos));
                    }
                    superSecciones.Add(new SuperSeccion(true, registro.entidad, seccionesLista.ToArray(), null));
                }
            }
            else
            {
                var datos = new Dato[]
                {
                    new Dato("Razon Social", razon_social),
                    new Dato("Resultado", antecedentes.message)
                };
                superSecciones.Add(new SuperSeccion(false, "Antecedentes", null, datos));
            }
            return superSecciones.ToArray();
        }

        public SuperSeccion[] CreateRepresentantesSections(PersonaFisica[] representantes_legales) 
        {
            List<SuperSeccion> listaSuperSecciones = new List<SuperSeccion>();
            foreach(var persona in representantes_legales)
            {
                var datos = new Dato[]
                {
                        new Dato("Nombres", persona.nombres),
                        new Dato("Apellido Paterno", persona.apellido_paterno),
                        new Dato("Apellido Materno", persona.apellido_materno),
                        new Dato("Nacionalidad", persona.nacionalidad),
                        new Dato("Fecha de Nacimiento", persona.fecha_nacimiento),
                        new Dato("rfc", persona.rfc),
                        new Dato("Genero", persona.genero),
                        new Dato("País de Residencia", persona.pais_residencia),
                        new Dato("País de Nacimiento", persona.pais_nacimiento),
                        new Dato("Entidad Federativa de Nacimiento", persona.entidad_federativa_nacimiento),
                        new Dato("Actividad Económica", persona.actividad_economica),
                        new Dato("Teléfono", persona.telefono),
                        new Dato("Correo Eletrónico", persona.correo_electronico),
                        new Dato("Curp", persona.curp)
                };
                listaSuperSecciones.Add(new SuperSeccion(false, persona.nombres + " " + persona.apellido_paterno +
                                " " + persona.apellido_materno, null, datos));
            }
            return listaSuperSecciones.ToArray();
        }

        public SuperSeccion[] CreateSociosFisicosSections(PersonaFisica[] sociosFisicos) 
        {
            List<SuperSeccion> superSectionsList = new List<SuperSeccion>();

            if (sociosFisicos is not null)
            {
                foreach (var socio in sociosFisicos)
                {
                    var data = new Dato[]
                    {
                        new Dato("Nombres", socio.nombres),
                        new Dato("Apellido Paterno", socio.apellido_paterno),
                        new Dato("Apellido Materno", socio.apellido_materno),
                        new Dato("Nacionalidad", socio.nacionalidad),
                        new Dato("Fecha de Nacimiento", socio.fecha_nacimiento),
                        new Dato("rfc", socio.rfc),
                        new Dato("Genero", socio.genero),
                        new Dato("País de Residencia", socio.pais_residencia),
                        new Dato("País de Nacimiento", socio.pais_nacimiento),
                        new Dato("Entidad Federativa de Nacimiento", socio.entidad_federativa_nacimiento),
                        new Dato("Actividad Económica", socio.actividad_economica),
                        new Dato("Teléfono", socio.telefono),
                        new Dato("Correo Eletrónico", socio.correo_electronico),
                        new Dato("Curp", socio.curp)
                    };
                    superSectionsList.Add(new SuperSeccion(false, socio.nombres + " " + socio.apellido_paterno +
                                " " + socio.apellido_materno, null, data));
                }
            }
            else
            {
                var data = new Dato[]
                {
                    new Dato("Resultado", "No se encontraron Socios Físicos")
                };
                superSectionsList.Add(new SuperSeccion(false, "Socios Físicos", null, data));
            }
            return superSectionsList.ToArray();
        }

        public SuperSeccion[] CreateSociosMoralesSections(PersonaMoral[] sociosMorales) 
        {
            List<SuperSeccion> superSectionsList = new List<SuperSeccion>();

            if (sociosMorales is not null)
            {
                foreach (var socio in sociosMorales)
                {
                    var data = new Dato[]
                    {
                        new Dato("Razon Social", socio.razon_social),
                        new Dato("RFC", socio.rfc),
                        new Dato("Participacion Social", (socio.participacion_social * 100).ToString() + "%"),
                        new Dato("Giro Mmercantil", socio.giro_mercantil),
                        new Dato("Nacionalidad", socio.nacionalidad),
                    };
                    superSectionsList.Add(new SuperSeccion(false, socio.razon_social, null, data));
                }
            }
            else
            {
                var data = new Dato[]
                {
                    new Dato("Resultado", "No se encontraron Socios Morales")
                };
                superSectionsList.Add(new SuperSeccion(false, "Socios Morales", null, data));
            }
            return superSectionsList.ToArray();
        }

    }
}
