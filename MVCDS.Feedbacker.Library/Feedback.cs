using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MVCDS.Feedbacker.Library
{
    [Serializable]
    /// <summary>
    /// Should be read to inform about a proccess
    /// </summary>
    public class Feedback
    {
        public const string EMPTY_FEEDBACK = "This feedback cannot be empty";

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
        /// Creates an instace of a feedback
        /// </summary>
        public Feedback()
            : this(EmptyResult.Allow)
        {
        }

        /// <summary>
        /// Creates an instance of a feedback
        /// </summary>
        /// <param name="force">Controls if the feedback is able to succeed without any results.
        public Feedback(EmptyResult force)
        {
            this.force = force;
            results = new List<Result>();
        }

        /// <summary>
        /// Creates an instance of a feedback 
        /// </summary>
        /// <param name="info">Data of serialization</param>
        /// <param name="context">Source of the feedback</param>
        public Feedback(SerializationInfo info, StreamingContext context)
        {
            results = (List<Result>)info.GetValue("result", typeof(List<Result>));
        }

        private readonly List<Result> results;
        /// <summary>
        /// Returns all the results the feedback has received
        /// When it's empty and the instance forbis empy results, the instance returns an error 
        /// </summary>
        public Result[] Results
        {
            get
            {
                if (ErrorOnEmpty)
                    throw new Exception(EMPTY_FEEDBACK);

                return results
                    .Where(p => p != null)
                    .ToArray();
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
                return !Bad.Any();
            }
        }

        public Result[] Bad
        {
            get
            {
                return Results
                    .Where(p => p.TriggersFailure)
                    .ToArray();
            }
        }

        public Result[] Good
        {
            get
            {
                return Results
                    .Where(p => !p.TriggersFailure)
                    .ToArray();
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("results", results, typeof(List<Result>));
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

        /// <summary>
        /// Add all the results from the source
        /// </summary>
        /// <param name="source">The feedback which will feed this instance</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Feed(Feedback source)
        {
            results.AddRange(source.Results);
            return this;
        }

        /// <summary>
        /// Add a non-standard result
        /// </summary>
        /// <param name="result">The result which will be added to this instance</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Feed(Result result)
        {
            results.Add(result);
            return this;
        }

        /// <summary>
        /// Add non-standard results
        /// </summary>
        /// <param name="values">The results which will be added to this instance</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Feed(params Result[] values)
        {
            results.AddRange(values);
            return this;
        }

        /// <summary>
        /// Add non-standard results
        /// </summary>
        /// <param name="values">The results which will be added to this instance</param>
        /// <returns>The instance to be chained</returns>
        public Feedback Feed(IEnumerable<Result> values)
        {
            results.AddRange(values);
            return this;
        }
    }
}
