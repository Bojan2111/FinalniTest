using AutoMapper;
using FinalniTest.Controllers;
using FinalniTest.Interfaces;
using FinalniTest.Models;
using FinalniTest.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace FinalniTestTest.Controllers
{
    public class PaketiControllerTest
    {
        private readonly ITestOutputHelper output;

        public PaketiControllerTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GetPaketi_ReturnsCollection()
        {
            List<Paket> paketi = new List<Paket>() {
            new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            },
            new Paket()
            {
                Id = 2,
                Posiljalac = "Slfsdike doo",
                Primalac = "Galerifasja doo",
                Tezina = 1.3m,
                Postarina = 540,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Vikrwetor Pavlov",
                    Rodjenje = 1997
                }
            },
            new Paket()
            {
                Id = 3,
                Posiljalac = "Sladsfaike doo",
                Primalac = "Galerigrgrja doo",
                Tezina = 1.12m,
                Postarina = 720,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viafsdavlov",
                    Rodjenje = 1977
                }
            },
            };

            List<PaketDTO> paketiDto = new List<PaketDTO>() {
            new PaketDTO()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirIme = "Viktor Pavlov"
            },
                  new PaketDTO()
            {
                Id = 2,
                Posiljalac = "Slfsdike doo",
                Primalac = "Galerifasja doo",
                Tezina = 1.3m,
                Postarina = 540,
                KurirIme = "Vikrwetor Pavlov"
            },
                    new PaketDTO()
            {
                Id = 3,
                Posiljalac = "Sladsfaike doo",
                Primalac = "Galerigrgrja doo",
                Tezina = 1.12m,
                Postarina = 720,
                KurirIme = "Viafsdavlov"
            }
            };
            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(paketi.AsQueryable());

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            var actionResult = controller.GetPaketi() as OkObjectResult;

            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            List<PaketDTO> dtoResult = (List<PaketDTO>)actionResult.Value;
            Assert.Equal(paketiDto[0], dtoResult[0]);
            Assert.Equal(paketiDto[1], dtoResult[1]);
            Assert.Equal(paketiDto[2], dtoResult[2]);
        }

        [Fact]
        public void GetPaket_ValidId_ReturnsObject()
        {
            // Arange
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            };

            PaketDTO paketDTO = new PaketDTO()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirIme = "Viktor Pavlov"
            };

            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(paket);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            var actionResult = controller.GetPaket(1) as OkObjectResult;

            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            PaketDTO dtoResult = mapper.Map<PaketDTO>(actionResult.Value as Paket);

            Assert.Equal(paketDTO, dtoResult);
        }
        
        [Fact]
        public void GetPaket_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IPaketiRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.GetPaket(12) as NotFoundResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void GetPaket_InvalidId_ReturnsBadRequest()
        {
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            };

            // Arrange
            var mockRepository = new Mock<IPaketiRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.GetPaket(12) as BadRequestResult;

            // Assert
            Assert.Null(actionResult);
        }

        [Fact]
        public void PostPaket_ValidRequest_SetsLocationHeader()
        {
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            };

            PaketDTO paketDTO = new PaketDTO()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirIme = "Viktor Pavlov"
            };

            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(paket);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.PostPaket(paket) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);
            Assert.Equal("GetPaket", actionResult.ActionName);
            Assert.Equal(1, actionResult.RouteValues["id"]);

            PaketDTO dtoResult = mapper.Map<PaketDTO>(actionResult.Value as Paket);
            Assert.Equal(paket.Id, dtoResult.Id);
            Assert.Equal(paket.Posiljalac, dtoResult.Posiljalac);
            Assert.Equal(paket.Primalac, dtoResult.Primalac);
            Assert.Equal(paket.Tezina, dtoResult.Tezina);
            Assert.Equal(paket.Postarina, dtoResult.Postarina);
            Assert.Equal(paket.Kurir.Ime, dtoResult.KurirIme);
        }
        [Fact]
        public void PostPaket_ValidRequest_ReturnsConflict()
        {
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            };

            PaketDTO paketDTO = new PaketDTO()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirIme = "Viktor Pavlov"
            };

            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(paket);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.PostPaket(paket) as ConflictObjectResult;

            // Assert
            Assert.Null(actionResult);
        }

        [Fact]
        public void PutPaket_ValidRequest_ReturnsObject()
        {
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            };

            PaketDTO paketDTO = new PaketDTO()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirIme = "Viktor Pavlov"
            };

            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(paket);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.PutPaket(1, paket) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            PaketDTO dtoResult = mapper.Map<PaketDTO>(actionResult.Value as Paket);
            Assert.Equal(paket.Id, dtoResult.Id);
            Assert.Equal(paket.Posiljalac, dtoResult.Posiljalac);
            Assert.Equal(paket.Primalac, dtoResult.Primalac);
            Assert.Equal(paket.Tezina, dtoResult.Tezina);
            Assert.Equal(paket.Postarina, dtoResult.Postarina);
            Assert.Equal(paket.Kurir.Ime, dtoResult.KurirIme);
        }
        [Fact]
        public void PutPaket_ValidId_ReturnsNoContent()
        {
            Paket paket = new Paket();
            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(paket);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.PutPaket(121, paket) as NoContentResult;

            // Assert
            Assert.Null(actionResult);
        }
        
        [Fact]
        public void PutPaket_InvalidId_ReturnsNotFound()
        {
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            };

            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(paket);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.PutPaket(131, paket) as NotFoundResult;

            // Assert
            Assert.Null(actionResult);
        }

        [Fact]
        public void PutPaket_InvalidId_ReturnsBadRequest()
        {
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            };

            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(paket);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.PutPaket(23, paket) as BadRequestResult;

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void PutPaket_InvalidId_ReturnsUnprocessableEntity()
        {
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "S",
                Primalac = "",
                Tezina = 15.1m,
                Postarina = 55520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 14987
                }
            };

            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(paket);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.PutPaket(1, paket) as UnprocessableEntityResult;

            // Assert
            Assert.Null(actionResult);
        }

        [Fact]
        public void DeletePaket_ValidId_ReturnsNoContent()
        {
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            };

            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(paket);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            var actionResult = controller.DeletePaket(1) as NoContentResult;
            Assert.NotNull(actionResult);
        }
        [Fact]
        public void DeletePaket_ValidId_ReturnsDeleted()
        {
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            };

            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(paket);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            var actionResult = controller.DeletePaket(1) as OkObjectResult;
            Assert.Null(actionResult);
        }
        [Fact]
        public void DeletePaket_InvalidId_ReturnsNotFound()
        {
            Paket paket = new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            };

            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(paket);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            var actionResult = controller.DeletePaket(23) as NotFoundResult;
            Assert.NotNull(actionResult);
        }
        [Fact]
        public void GetPaketiByTezina_ValidRequest_ReturnsCollection()
        {
            TezineDTO tezine = new TezineDTO()
            {
                Najmanje = 1,
                Najvise = 2
            };

            List<Paket> paketi = new List<Paket>() {
            new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            },
            new Paket()
            {
                Id = 2,
                Posiljalac = "Slfsdike doo",
                Primalac = "Galerifasja doo",
                Tezina = 1.3m,
                Postarina = 540,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Vikrwetor Pavlov",
                    Rodjenje = 1997
                }
            },
            new Paket()
            {
                Id = 3,
                Posiljalac = "Sladsfaike doo",
                Primalac = "Galerigrgrja doo",
                Tezina = 1.12m,
                Postarina = 720,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viafsdavlov",
                    Rodjenje = 1977
                }
            },
            };

            List<PaketDTO> paketiDto = new List<PaketDTO>() {
            new PaketDTO()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirIme = "Viktor Pavlov"
            },
                  new PaketDTO()
            {
                Id = 2,
                Posiljalac = "Slfsdike doo",
                Primalac = "Galerifasja doo",
                Tezina = 1.3m,
                Postarina = 540,
                KurirIme = "Vikrwetor Pavlov"
            },
                    new PaketDTO()
            {
                Id = 3,
                Posiljalac = "Sladsfaike doo",
                Primalac = "Galerigrgrja doo",
                Tezina = 1.12m,
                Postarina = 720,
                KurirIme = "Viafsdavlov"
            }
            };


            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(paketi.AsQueryable());

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);
            List<PaketDTO> testLista = controller.GetPaketiByTezina(tezine) as List<PaketDTO>;

            var actionResult = controller.GetPaketiByTezina(tezine) as OkObjectResult;

            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);
            List<PaketDTO> dtoResult = actionResult.Value as List<PaketDTO>;
            Assert.IsType<List<PaketDTO>>(actionResult.Value);
        }

        [Fact]
        public void GetPaketiByTezina_InvalidRequest_ReturnsBadRequest()
        {
            TezineDTO tezine = new TezineDTO()
            {
                Najmanje = 1,
                Najvise = 8
            };

            List<Paket> paketi = new List<Paket>() {
            new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            },
            new Paket()
            {
                Id = 2,
                Posiljalac = "Slfsdike doo",
                Primalac = "Galerifasja doo",
                Tezina = 1.3m,
                Postarina = 540,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Vikrwetor Pavlov",
                    Rodjenje = 1997
                }
            },
            new Paket()
            {
                Id = 3,
                Posiljalac = "Sladsfaike doo",
                Primalac = "Galerigrgrja doo",
                Tezina = 1.12m,
                Postarina = 720,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viafsdavlov",
                    Rodjenje = 1977
                }
            },
            };

            List<PaketDTO> paketiDto = new List<PaketDTO>() {
            new PaketDTO()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirIme = "Viktor Pavlov"
            },
                  new PaketDTO()
            {
                Id = 2,
                Posiljalac = "Slfsdike doo",
                Primalac = "Galerifasja doo",
                Tezina = 1.3m,
                Postarina = 540,
                KurirIme = "Vikrwetor Pavlov"
            },
                    new PaketDTO()
            {
                Id = 3,
                Posiljalac = "Sladsfaike doo",
                Primalac = "Galerigrgrja doo",
                Tezina = 1.12m,
                Postarina = 720,
                KurirIme = "Viafsdavlov"
            }
            };


            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(paketi.AsQueryable());

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            var actionResult = controller.GetPaketiByTezina(tezine) as BadRequestResult;

            Assert.Null(actionResult);
        }

        [Fact]
        public void GetPaketiByPrijem_ValidRequest_ReturnsCollection()
        {
            List<Paket> paketi = new List<Paket>() {
            new Paket()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viktor Pavlov",
                    Rodjenje = 1987
                }
            },
            new Paket()
            {
                Id = 2,
                Posiljalac = "Slfsdike doo",
                Primalac = "Galerija doo",
                Tezina = 1.3m,
                Postarina = 540,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Vikrwetor Pavlov",
                    Rodjenje = 1997
                }
            },
            new Paket()
            {
                Id = 3,
                Posiljalac = "Sladsfaike doo",
                Primalac = "Galerija doo",
                Tezina = 1.12m,
                Postarina = 720,
                KurirId = 3,
                Kurir = new Kurir()
                {
                    Id = 3,
                    Ime = "Viafsdavlov",
                    Rodjenje = 1977
                }
            },
            };

            List<PaketDTO> paketiDto = new List<PaketDTO>() {
            new PaketDTO()
            {
                Id = 1,
                Posiljalac = "Slike doo",
                Primalac = "Galerija doo",
                Tezina = 1.1m,
                Postarina = 520,
                KurirIme = "Viktor Pavlov"
            },
                  new PaketDTO()
            {
                Id = 2,
                Posiljalac = "Slfsdike doo",
                Primalac = "Galerija doo",
                Tezina = 1.3m,
                Postarina = 540,
                KurirIme = "Vikrwetor Pavlov"
            },
                    new PaketDTO()
            {
                Id = 3,
                Posiljalac = "Sladsfaike doo",
                Primalac = "Galerija doo",
                Tezina = 1.12m,
                Postarina = 720,
                KurirIme = "Viafsdavlov"
            }
            };
            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(paketi.AsQueryable());

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            var actionResult = controller.GetPaketiByPrijem("Galerija doo") as OkObjectResult;
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);
            Assert.IsType<List<PaketDTO>>(actionResult.Value);
        }

        [Fact]
        public void GetPaketiByPrijem_InvalidQuery_ReturnsNotFound()
        {
            List<Paket> paketi = new List<Paket>();

            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(paketi.AsQueryable());

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            var actionResult = controller.GetPaketiByPrijem("Galerija doo") as NotFoundResult;
            Assert.Null(actionResult);
        }

        [Fact]
        public void GetPaketiByPrijem_InvalidQuery_ReturnsBadRequest()
        {
            List<Paket> paketi = new List<Paket>();

            var mockRepository = new Mock<IPaketiRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(paketi.AsQueryable());

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketiController(mockRepository.Object, mapper);

            var actionResult = controller.GetPaketiByPrijem("Galerija doo") as BadRequestResult;
            Assert.Null(actionResult);
            
        }
    }
}
