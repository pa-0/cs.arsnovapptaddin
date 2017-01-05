﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ARSnovaPPIntegration.Business.Model;
using ARSnovaPPIntegration.Presentation.Commands;
using ARSnovaPPIntegration.Common.Enum;
using ARSnovaPPIntegration.Presentation.Window;

namespace ARSnovaPPIntegration.Presentation.Models
{
    public class SelectArsnovaTypeViewViewModel : BaseViewModel
    {
        public SelectArsnovaTypeViewViewModel(ViewModelRequirements requirements)
            : base(requirements)
        {
            this.InitializeWindowCommandBindings();

            if (string.IsNullOrEmpty(this.SlideSessionModel.Hashtag))
            {
                this.SlideSessionModel.Hashtag = Globals.ThisAddIn.Application.ActivePresentation.Name;
            }
        }

        public bool IsArsnovaClickSession
        {
            get { return this.SlideSessionModel.SessionType == SessionType.ArsnovaClick; }
            set
            {
                if (value)
                {
                    if (this.SlideSessionModel.Questions.Any())
                    {
                        var reset = PopUpWindow.ConfirmationWindow(
                            this.LocalizationService.Translate("Reset"),
                            this.LocalizationService.Translate(
                                    "If this value is changed, other Session-Properties like the answer options or the question type will be reseted. Do you want to continue?"));

                        if (reset)
                        {
                            this.SlideSessionModel.SessionType = SessionType.ArsnovaClick;
                            this.OnSessionTypeSelectionChanged();
                            this.SlideSessionModel.Questions = new ObservableCollection<SlideQuestionModel>();
                        }
                    }
                    else
                    {
                        this.SlideSessionModel.SessionType = SessionType.ArsnovaClick;
                        this.OnSessionTypeSelectionChanged();
                    }
                } 
            }
        }

        public string Hashtag
        {
            get { return this.SlideSessionModel.Hashtag; }
            set { this.SlideSessionModel.Hashtag = value; }
        }

        public bool IsArsnovaVotingSession
        {
            get { return this.SlideSessionModel.SessionType == SessionType.ArsnovaVoting; }
            set
            {
                if (value)
                {
                    if (this.SlideSessionModel.Questions.Any())
                    {
                        var reset = PopUpWindow.ConfirmationWindow(
                            this.LocalizationService.Translate("Reset"),
                            this.LocalizationService.Translate(
                                    "If this value is changed, other Session-Properties like the answer options or the question type will be reseted. Do you want to continue?"));

                        if (reset)
                        {
                            this.SlideSessionModel.SessionType = SessionType.ArsnovaVoting;
                            this.OnSessionTypeSelectionChanged();
                            this.SlideSessionModel.Questions = new ObservableCollection<SlideQuestionModel>();
                        }
                    }
                    else
                    {
                        this.SlideSessionModel.SessionType = SessionType.ArsnovaVoting;
                        this.OnSessionTypeSelectionChanged();
                    }
                }
            }
        }

        public string Header => this.LocalizationService.Translate("New question");

        public string Text
            =>
            this.LocalizationService.Translate(
                    "Which type of question do you want to ask? Arsnova.voting is the serious, grown up one while arsnova.click is faster, more colorful and crammed up with gamification.");

        private void OnSessionTypeSelectionChanged()
        {
            this.OnPropertyChanged(nameof(this.IsArsnovaClickSession));
            this.OnPropertyChanged(nameof(this.IsArsnovaVotingSession));
        }

        private void InitializeWindowCommandBindings()
        {
            this.WindowCommandBindings.AddRange(
                    new List<CommandBinding>
                    {
                        new CommandBinding(
                            NavigationButtonCommands.Forward,
                            (e, o) =>
                            {
                                this.ViewPresenter.Show(
                                    new SessionOverviewViewViewModel(this.GetViewModelRequirements()));
                            },
                            (e, o) => o.CanExecute = true)
                    });
        }
    }
}