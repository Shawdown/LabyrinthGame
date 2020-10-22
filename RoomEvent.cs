using System.Collections.Generic;

namespace LabyrinthGame
{
    public class RoomEvent
    {
        public string Question { get; }
        public List<string> Answers { get; }
        public List<Item> Rewards { get; }
        public int Punishment { get; }
        public RoomEvent(string question, List<string> answers, List<Item> rewards, int punishment)
        {
            Question = question;
            Answers = answers;
            Rewards = rewards;
            Punishment = punishment;
        }

        // Check if the answer is correct. Returns: true if correct, false otherwise.
        public bool CheckAnswer(string answer)
        {
            return Answers.Contains(answer.ToLower());
        }
    }
}