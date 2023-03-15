using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Service.DTOs.Insight
{
    public class QuizTopScoreInsightDto
    {
        /// <summary>
        /// 5 highest scores
        /// </summary>
        public int QuizResultGoodsCount { get; set; }

        /// <summary>
        /// 4 normal score
        /// </summary>
        public int QuizResultNormalsCount { get; set; }

        /// <summary>
        /// 3 bad score
        /// </summary>
        public int QuizResultBadsCount { get; set; }
    }
}
