using System.Collections.Generic;

namespace Synchronized.Domain.Factories.Interfaces
{
    /// <summary>
    /// This interface defines methods for creating new Database Entities.
    /// </summary>
    public interface IDomainModelFactory
    {
        /// <summary>
        /// A generic method for getting a new instance of any Type.
        /// </summary>
        /// <typeparam name="T">The type of which we need a new instance.</typeparam>
        /// <returns>A new instace of T</returns>
        T GetOfType<T>();

        /// <summary>
        /// A method for getting a new instance of Type Question.
        /// </summary>
        /// <returns>A new instance of Question.</returns>
        Question GetQuestion();

        /// <summary>
        /// A method for gerring a new instance of QuestionsList
        /// </summary>
        /// <returns>A new instance of QuestionsList</returns>
        List<Question> GetQuestionsList();

        /// <summary>
        /// A method for getting a new instance of Type Answer.
        /// </summary>
        /// <returns>A new instance of Answer</returns>
        Answer GetAnswer();

        /// <summary>
        /// A method for getting a new instance of Type Comment.
        /// </summary>
        /// <returns>A new instance of Comment.</returns>
        Comment GetComment();

        /// <summary>
        /// A method for getting a new instance of type QuestionTag.
        /// </summary>
        /// <returns>A new instance of QuestionTag.</returns>
        QuestionTag GetQuestionTag();
    
    }
}
