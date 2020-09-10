﻿using System.Linq;

namespace Nodify.StateMachine
{
    public class BlackboardViewModel : ObservableObject
    {
        public NodifyObservableCollection<BlackboardKeyViewModel> Keys { get; } = new NodifyObservableCollection<BlackboardKeyViewModel>();

        // TODO: Load from assembly based on attributes
        public NodifyObservableCollection<ActionViewModel> Actions { get; } = new NodifyObservableCollection<ActionViewModel>
        {
            new ActionViewModel
            {
                Name = nameof(CopyBlackboardKeyAction)
            }
        };

        public INodifyCommand AddKeyCommand { get; }

        public BlackboardViewModel()
        {
            Keys.WhenAdded(key =>
            {
                var existingKeyNames = Keys.Where(k => k != key).Select(k => k.Name).ToList();
                key.Name = existingKeyNames.GetUnique(key.Name);
            });

            AddKeyCommand = new DelegateCommand(() => Keys.Add(new BlackboardKeyViewModel
            {
                Name = "New Key "
            }));
        }
    }
}
