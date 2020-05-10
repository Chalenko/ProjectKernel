using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes
{
    /// <summary>
    /// Это исключение выбрасывается, если объект не существует в системе
    /// </summary>
    public class ObjectNotExistException : ArgumentException
    {
        /// <summary>
        /// Выполняет инициализацию нового экземпляра класса ProjectKernel.Classes.User.ObjectNotExistException с указанным сообщением об ошибке.
        /// </summary>
        /// <param name="message">Сообщение об ошибке с объяснением причины исключения.</param>
        public ObjectNotExistException(string message) : base(message) { }

        /// <summary>
        /// Выполняет инициализацию нового экземпляра класса ProjectKernel.Classes.User.ObjectNotExistException с указанным сообщением об ошибке и ссылкой на внутреннее исключение, которое стало причиной данного исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке с объяснением причины исключения.</param>
        /// <param name="innerException">Исключение, являющееся причиной текущего исключения.Если параметр innerException не является указателем NULL, текущее исключение вызывается в блоке catch, обрабатывающем внутренние исключения.</param>
        public ObjectNotExistException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Это исключение выбрасывается, если объект уже существует в системе
    /// </summary>
    public class ObjectAlreadyExistException : ArgumentException
    {
        /// <summary>
        /// Выполняет инициализацию нового экземпляра класса ProjectKernel.Classes.User.ObjectAlreadyExistException с указанным сообщением об ошибке.
        /// </summary>
        /// <param name="message">Сообщение об ошибке с объяснением причины исключения.</param>
        public ObjectAlreadyExistException(string message) : base(message) { }

        /// <summary>
        /// Выполняет инициализацию нового экземпляра класса ProjectKernel.Classes.User.ObjectAlreadyExistException с указанным сообщением об ошибке и ссылкой на внутреннее исключение, которое стало причиной данного исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке с объяснением причины исключения.</param>
        /// <param name="innerException">Исключение, являющееся причиной текущего исключения.Если параметр innerException не является указателем NULL, текущее исключение вызывается в блоке catch, обрабатывающем внутренние исключения.</param>
        public ObjectAlreadyExistException(string message, Exception innerException) : base(message, innerException) { }
    }
}
