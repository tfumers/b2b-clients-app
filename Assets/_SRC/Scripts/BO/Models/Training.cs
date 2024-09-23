public class Training
{
    private long id;

    private string difficulty;

    private string name;

    private string description;

    private string videoUrl;

    private TrainingVideo trainingVideo;

    private string imageUrl;

    private TrainingImage trainingImage;

    private int estTimePerRep;

    private int estCaloriesPerRep;

    public Training(SimpleJSON.JSONObject json)
    {
        this.id = json["id"];
        this.difficulty = json["difficulty"];
        this.name = json["name"];
        this.description = json["description"];
        this.videoUrl = json["videoUrl"];
        this.imageUrl = json["imageUrl"];
        this.estTimePerRep = json["estTimePerRep"].AsInt;
        this.estCaloriesPerRep = json["estCaloriesPerRep"].AsInt;
    }

    public long Id { get => id; set => id = value; }
    public string Difficulty { get => difficulty; set => difficulty = value; }
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public string VideoUrl { get => videoUrl; set => videoUrl = value; }
    public string ImageUrl { get => imageUrl; set => imageUrl = value; }
    public int EstTimePerRep { get => estTimePerRep; set => estTimePerRep = value; }
    public int EstCaloriesPerRep { get => estCaloriesPerRep; set => estCaloriesPerRep = value; }
    public TrainingVideo TrainingVideo { get => trainingVideo; set => trainingVideo = value; }
    public TrainingImage TrainingImage { get => trainingImage; set => trainingImage = value; }
}
