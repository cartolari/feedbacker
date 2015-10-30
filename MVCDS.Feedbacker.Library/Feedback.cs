using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCDS.Feedbacker.Library
{
    /// <summary>
    /// Should be read to inform about a proccess
    /// </summary>
    public class Feedback
    {
        /// <summary>
        /// When <value>Allow</value> is set, the feedback is able to succeed even when there's no results to read
        /// If otherwise, the feedback will fail without at least one result
        /// </summary>
        public enum EmptyResult
        {
            Allow,
            Forbid
        }

        /// <summary>
        /// Creates an instance of a feedback
        /// </summary>
        /// <param name="force">Controls if the feedback is able to succeed without any results.
        /// The default is <code>Allow</code></param>
        public Feedback(EmptyResult force = EmptyResult.Allow)
        {
            this.force = force;
            results = new List<IResult>();
        }

        private readonly List<IResult> results;
        /// <summary>
        /// Returns all the results the feedback has received
        /// When it's empty and the instance forbis empy results, the instance returns an error 
        /// </summary>
        public IResult[] Results
        {
            get
            {
                if (ErrorOnEmpty)
                {
                    return new IResult[] {
                        new Error("This feedback cannot be empty")
                    };
                }

                return results.ToArray();
            }
        }

        private readonly EmptyResult force;
        private bool ErrorOnEmpty
        {
            get
            {
                return force == EmptyResult.Forbid && !results.Any();
            }
        }

        /// <summary>
        /// A feedback is successfull if there are no results which triggers its failure
        /// </summary>
        public bool Success
        {
            get
            {
                return !Results.Any(p => p.TriggersFailure);
            }
        }

        /// <summary>
        /// Add an error
        /// </summary>
        /// <param name="exception">The error</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Error(Exception exception)
        {
            results.Add(new Error(exception));
            return this;
        }

        /// <summary>
        /// Add an error
        /// </summary>
        /// <param name="message">The error message</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Error(string message)
        {
            results.Add(new Error(message));
            return this;
        }

        /// <summary>
        /// Add an error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exception">The error</param>
        /// <param name="value">A value which the error is concerned with</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Error<T>(Exception exception, T value)
        {
            results.Add(new Error<T>(exception, value));
            return this;
        }


        /// <summary>
        /// Add an error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The error message</param>
        /// <param name="value">A value which the error is concerned with</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Error<T>(string message, T value)
        {
            results.Add(new Error<T>(message, value));
            return this;
        }

        /// <summary>
        /// Add an information
        /// </summary>
        /// <param name="message">The information message</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Inform(string message)
        {
            results.Add(new Information(message));
            return this;
        }

        /// <summary>
        /// Add an information
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The information message</param>
        /// <param name="value">A value which the information is concerned with</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Inform<T>(string message, T value)
        {
            results.Add(new Information<T>(message, value));
            return this;
        }

        /// <summary>
        /// Add an observation
        /// </summary>
        /// <param name="message">The observation message</param>
        /// <param name="failure">Does the observation triggers the instance's failure?</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Observe(string message, bool failure = false)
        {
            results.Add(new Observation(message, failure));
            return this;
        }
        /// <summary>
        /// Add an observation which may trigger the failure or not
        /// </summary>
        /// <param name="message">The observation message</param>
        /// <param name="failure">Callback which may trigger the failure</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Observe(string message, Func<bool> failure)
        {
            results.Add(new Observation(message, failure));
            return this;
        }

        /// <summary>
        /// Add an observation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The observation message</param>
        /// <param name="value">A value which the observation is concerned with</param>
        /// <param name="failure">Does the observation triggers the instance's failure?</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Observe<T>(string message, T value, bool failure = false)
        {
            results.Add(new Observation<T>(message, failure, value));
            return this;
        }

        /// <summary>
        /// Add an observation which may trigger the failure or not
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The observation message</param>
        /// <param name="value">A value which the observation is concerned with</param>
        /// <param name="failure">Callback which may trigger the failure</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Observe<T>(string message, T value, Func<bool> failure)
        {
            results.Add(new Observation<T>(message, failure, value));
            return this;
        }
    }
}
