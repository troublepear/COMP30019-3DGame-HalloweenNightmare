
// This class contains metadata for your submission. It plugs into some of our
// grading tools to extract your game/team details. Ensure all Gradescope tests
// pass when submitting, as these do some basic checks of this file.
public static class SubmissionInfo
{
    // TASK: Fill out all team + team member details below by replacing the
    // content of the strings. Also ensure you read the specification carefully
    // for extra details related to use of this file.

    // URL to your group's project 2 repository on GitHub.
    public static readonly string RepoURL = "https://github.com/COMP30019/project-2-le-meilleur";
    
    // Come up with a team name below (plain text, no more than 50 chars).
    public static readonly string TeamName = "Le Meilleur";
    
    // List every team member below. Ensure student names/emails match official
    // UniMelb records exactly (e.g. avoid nicknames or aliases).
    public static readonly TeamMember[] Team = new[]
    {
        new TeamMember("Suijuan Wang", "suijuanw@student.unimelb.edu.au"),
        new TeamMember("Min Shen", "msshe1@student.unimelb.edu.au"),
        new TeamMember("Zizhou Zhang", "zizhzhang@student.unimelb.edu.au"),
        new TeamMember("Yurun Wang", "yurunw@student.unimelb.edu.au"), 
    };

    // This may be a "working title" to begin with, but ensure it is final by
    // the video milestone deadline (plain text, no more than 50 chars).
    public static readonly string GameName = "Halloween Nightmare";

    // Write a brief blurb of your game, no more than 200 words. Again, ensure
    // this is final by the video milestone deadline.
    public static readonly string GameBlurb = 
@"
You entered somewhere while you were asleep ...
Welcome to Diagon Alley of the Wizarding World on Halloween!
This is a dark and seedy alley which is frequently populated by Dark Wizards.
Watch out! There're also dark wizard stores, terrifying graveyards, frightening ghosts and dangerous skeletons here. Of course, the danger lies everywhere in Diagon Alley.
Immerse yourself in the nightmares of Halloween, a dark and whimsical tale that will confront you with your Halloween fears!
Escape the nightmare - a mysterious Wizarding World inhabited by corrupted souls looking for their next meals.
";
    
    // By the gameplay video milestone deadline this should be a direct link
    // to a YouTube video upload containing your video. Ensure "Made for kids"
    // is turned off in the video settings. 
    public static readonly string GameplayVideo = "https://www.youtube.com/watch?v=SDrILspGqwA&ab_channel=AshleyWang";
    
    // No more info to fill out!
    // Please don't modify anything below here.
    public readonly struct TeamMember
    {
        public TeamMember(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}
