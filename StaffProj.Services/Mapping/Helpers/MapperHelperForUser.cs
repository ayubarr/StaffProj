using StaffProj.ApiModels.DTOs.BaseDTOs;
using StaffProj.Domain.Models.Abstractions.BaseEntities;
using StaffProj.Domain.Models.Abstractions.BaseUsers;
using StaffProj.Services.Mapping.Config;
using StaffProj.ValidationHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffProj.Services.Mapping.Helpers
{
    /// <summary>
    /// Helper class for mapping between model objects and entity objects using AutoMapper.
    /// </summary>
    /// <typeparam name="Tmodel">The model type, derived from BaseEntityDTO.</typeparam>
    /// <typeparam name="T">The entity type, derived from BaseEntity.</typeparam>
    public class MapperHelperForUser<Tmodel, T>
        where Tmodel : ApplicationUserDTO
        where T : ApplicationUser
    {
        /// <summary>
        /// Maps a collection of model objects to a collection of entity objects.
        /// </summary>
        /// <param name="sourceCollection">The source collection of model objects.</param>
        /// <returns>The mapped collection of entity objects.</returns>
        public static IEnumerable<T> Map(IEnumerable<Tmodel> sourceCollection)
        {
            ObjectValidator<IEnumerable<Tmodel>>.CheckIsNotNullObject(sourceCollection);

            var mapper = AutoMapperConfig<Tmodel, T>.Initialize();
            return mapper.Map<IEnumerable<T>>(sourceCollection);
        }

        /// <summary>
        /// Maps a model object to an entity object.
        /// </summary>
        /// <param name="source">The source model object.</param>
        /// <returns>The mapped entity object.</returns>
        public static T Map(Tmodel source)
        {
            ObjectValidator<Tmodel>.CheckIsNotNullObject(source);
            var mapper = AutoMapperConfig<Tmodel, T>.Initialize();
            return mapper.Map<T>(source);
        }
    }
}
