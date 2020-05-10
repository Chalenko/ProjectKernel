using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKernel.Classes.Logger
{
    /// <summary>
    /// Уровень логгируемого сообщения
    /// </summary>
    /// <remarks>
    /// <para>Что означает каждый уровень?</para>
    /// <para>Debug: сообщения отладки, профилирования. В production системе обычно сообщения этого уровня включаются при первоначальном запуске системы или для поиска узких мест (bottleneck-ов).</para>
    /// <para>Info: обычные сообщения, информирующие о действиях системы. Реагировать на такие сообщения вообще не надо, но они могут помочь, например, при поиске багов, расследовании интересных ситуаций итд.</para>
    /// <para>Warn: записывая такое сообщение, система пытается привлечь внимание обслуживающего персонала. Произошло что-то странное. Возможно, это новый тип ситуации, ещё не известный системе. Следует разобраться в том, что произошло, что это означает, и отнести ситуацию либо к инфо-сообщению, либо к ошибке. Соответственно, придётся доработать код обработки таких ситуаций.</para>
    /// <para>Error: ошибка в работе системы, требующая вмешательства. Что-то не сохранилось, что-то отвалилось. Необходимо принимать меры довольно быстро! Ошибки этого уровня и выше требуют немедленной записи в лог, чтобы ускорить реакцию на них. Нужно понимать, что ошибка пользователя – это не ошибка системы. Если пользователь ввёл в поле -1, где это не предполагалось – не надо писать об этом в лог ошибок.</para>
    /// <para>Fatal: это особый класс ошибок. Такие ошибки приводят к неработоспособности системы в целом, или неработоспособности одной из подсистем. Чаще всего случаются фатальные ошибки из-за неверной конфигурации или отказов оборудования. Требуют срочной, немедленной реакции. Возможно, следует предусмотреть уведомление о таких ошибках по SMS.</para>
    /// </remarks>
    /// <see href="https://habrahabr.ru/post/98638/"/>
    public enum LogLevel
    {
        /// <summary>
        /// Отладочная информация
        /// </summary>
        Debug, 
        /// <summary>
        /// Информация о действиях пользователя
        /// </summary>
        Info, 
        /// <summary>
        /// Предупреждение. Приложение можето продолжать работу
        /// </summary>
        Warn, 
        /// <summary>
        /// Ошибка. Приложение может продолжать работу
        /// </summary>
        Error, 
        /// <summary>
        /// Критическая ошибка. Приложение не может работать далее без исправления ошибки
        /// </summary>
        Fatal
    }

    /// <summary>
    /// Стандартный интерфейс логгирования
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Запись данных в хранилище логов
        /// </summary>
        /// <param name="logData">Данные для логгирования</param>
        void log(LogData logData);

        /// <summary>
        /// Запись текста лога в хранилище логов с указанием уровня лога
        /// </summary>
        /// <param name="lvl">Уровень лога</param>
        /// <param name="text">Текст лога</param>
        void log(LogLevel lvl, string text);

        /// <summary>
        /// Запись текста лога в хранилище логов с указанием уровня лога и ошибкой связанной с этим логом
        /// </summary>
        /// <param name="lvl">Уровень лога</param>
        /// <param name="text">текст лога</param>
        /// <param name="ex">Ошибка связанная с логом</param>
        void log(LogLevel lvl, string text, Exception ex);

        /// <summary>
        /// Запись текста в лог с <see cref="ProjectKernel.Classes.Logger.LogLevel">уровнем</see> Debug
        /// </summary>
        /// <param name="text">Текст лога</param>
        void debug(string text);

        /// <summary>
        /// Запись лога с исключением с <see cref="ProjectKernel.Classes.Logger.LogLevel">уровнем</see> Debug
        /// </summary>
        /// <param name="text">Текст лога</param>
        /// <param name="ex">Логгируемое исключение</param>
        void debug(string text, Exception ex);

        /// <summary>
        /// Запись текста в лог с <see cref="ProjectKernel.Classes.Logger.LogLevel">уровнем</see> Info
        /// </summary>
        /// <param name="text">Текст лога</param>
        void info(string text);

        /// <summary>
        /// Запись лога с исключением с <see cref="ProjectKernel.Classes.Logger.LogLevel">уровнем</see> Info
        /// </summary>
        /// <param name="text">Текст лога</param>
        /// <param name="ex">Логгируемое исключение</param>
        void info(string text, Exception ex);

        /// <summary>
        /// Запись текста в лог с <see cref="ProjectKernel.Classes.Logger.LogLevel">уровнем</see> Warn
        /// </summary>
        /// <param name="text">Текст лога</param>
        void warn(string text);

        /// <summary>
        /// Запись лога с исключением с <see cref="ProjectKernel.Classes.Logger.LogLevel">уровнем</see> Warn
        /// </summary>
        /// <param name="text">Текст лога</param>
        /// <param name="ex">Логгируемое исключение</param>
        void warn(string text, Exception ex);

        /// <summary>
        /// Запись текста в лог с <see cref="ProjectKernel.Classes.Logger.LogLevel">уровнем</see> Error
        /// </summary>
        /// <param name="text">Текст лога</param>
        void error(string text);// { log(LogLevel.Error, text); }

        /// <summary>
        /// Запись лога с исключением с <see cref="ProjectKernel.Classes.Logger.LogLevel">уровнем</see> Error
        /// </summary>
        /// <param name="text">Текст лога</param>
        /// <param name="ex">Логгируемое исключение</param>
        void error(string text, Exception ex);// { log(LogLevel.Error, text, ex); }

        /// <summary>
        /// Запись текста в лог с <see cref="ProjectKernel.Classes.Logger.LogLevel">уровнем</see> Fatal
        /// </summary>
        /// <param name="text">Текст лога</param>
        void fatal(string text);// { log(LogLevel.Fatal, text); }

        /// <summary>
        /// Запись лога с исключением с <see cref="ProjectKernel.Classes.Logger.LogLevel">уровнем</see> Fatal
        /// </summary>
        /// <param name="text">Текст лога</param>
        /// <param name="ex">Логгируемое исключение</param>
        void fatal(string text, Exception ex);// { log(LogLevel.Fatal, text, ex); }
    }
}
