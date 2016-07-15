using System;
using System.ComponentModel.DataAnnotations;

namespace Aliencube.EntityFrameworkCore.Extensions.Tests.Fixtures
{
    /// <summary>
    /// This represents the model entity for Foo.
    /// </summary>
    public class Foo
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the mapped value.
        /// </summary>
        public string AlreadyMappedByOthers { get; set; }

        /// <summary>
        /// Gets or seets the annotated string value.
        /// </summary>
        [MaxLength(200)]
        public string AnnotatedString { get; set; }

        /// <summary>
        /// Gets or sets the fluent API string value.
        /// </summary>
        public string FluentApiString { get; set; }
    }
}