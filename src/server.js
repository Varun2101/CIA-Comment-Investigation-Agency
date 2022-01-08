import express from "express";
import { google } from "googleapis";

const app = express();
//app.use(express.json());
app.use(express.urlencoded({ extended: true }));

app.post("/getCommentToxicity", (req, res) => {
  const API_KEY = "YOUR_API_KEY";
  const DISCOVERY_URL =
    "https://commentanalyzer.googleapis.com/$discovery/rest?version=v1alpha1";

  google
    .discoverAPI(DISCOVERY_URL)
    .then((client) => {
      console.log(req.body);
      const analyzeRequest = {
        comment: {
          text: req.body.comment,
        },
        requestedAttributes: {
          TOXICITY: {},
        },
      };

      client.comments.analyze(
        {
          key: API_KEY,
          resource: analyzeRequest,
        },
        (err, response) => {
          if (err) throw err;
          console.log(JSON.stringify(response.data, null, 2));
          const data = response.data;
          console.log(data);
          const toxicity =
            data["attributeScores"]["TOXICITY"]["spanScores"][0]["score"][
              "value"
            ];
          res.status(200).json({
            toxicity: toxicity,
          });
        }
      );
    })
    .catch((err) => {
      throw err;
    });
});

app.listen(3000, () => {
  console.log("Listening on port 3000");
});
