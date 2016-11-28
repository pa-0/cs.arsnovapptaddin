﻿using Microsoft.Office.Interop.PowerPoint;

using ARSnovaPPIntegration.Common.Enum;

namespace ARSnovaPPIntegration.Business.Model
{
    public class SlideSessionModel
    {
        public SlideSessionModel(Slide slide)
        {
            this.Slide = slide;
        }

        // TODO: Concept: which default values should be taken?

        public Slide Slide { get; set; }

        public SessionType SessionType { get; set; } = SessionType.ArsnovaClick;

        public QuestionTypeEnum QuestionType { get; set; }

        public string QuestionText { get; set; }
    }
}