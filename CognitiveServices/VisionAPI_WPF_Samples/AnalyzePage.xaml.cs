﻿// 
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license.
// 
// Microsoft Cognitive Services (formerly Project Oxford): https://www.microsoft.com/cognitive-services
// 
// Microsoft Cognitive Services (formerly Project Oxford) GitHub:
// https://github.com/Microsoft/Cognitive-Vision-Windows
// 
// Copyright (c) Microsoft Corporation
// All rights reserved.
// 
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using System;
using System.IO;
using System.Threading.Tasks;

// -----------------------------------------------------------------------
// KEY SAMPLE CODE STARTS HERE
// Use the following namesapce for VisionServiceClient
// -----------------------------------------------------------------------
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
// -----------------------------------------------------------------------
// KEY SAMPLE CODE ENDS HERE
// -----------------------------------------------------------------------

namespace VisionAPI_WPF_Samples
{
    /// <summary>
    /// Interaction logic for AnalyzePage.xaml
    /// </summary>
    public partial class AnalyzePage : ImageScenarioPage
    {
        public AnalyzePage()
        {
            InitializeComponent();
            this.PreviewImage = _imagePreview;
            this.URLTextBox = _urlTextBox;
        }

        /// <summary>
        /// Uploads the image to Project Oxford and performs analysis
        /// </summary>
        /// <param name="imageFilePath">The image file path.</param>
        /// <returns></returns>
        private async Task<AnalysisResult> UploadAndAnalyzeImage(string imageFilePath)
        {
            // -----------------------------------------------------------------------
            // KEY SAMPLE CODE STARTS HERE
            // -----------------------------------------------------------------------

            //
            // Create Project Oxford Vision API Service client
            //
            VisionServiceClient VisionServiceClient = new VisionServiceClient(SubscriptionKey, SubscriptionEndpoint);
            Log("VisionServiceClient is created");

            using (Stream imageFileStream = File.OpenRead(imageFilePath))
            {
                //
                // Analyze the image for all visual features
                //
                Log("Calling VisionServiceClient.AnalyzeImageAsync()...");
                VisualFeature[] visualFeatures = new VisualFeature[] { VisualFeature.Adult, VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description, VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags };
                AnalysisResult analysisResult = await VisionServiceClient.AnalyzeImageAsync(imageFileStream, visualFeatures);
                return analysisResult;
            }

            // -----------------------------------------------------------------------
            // KEY SAMPLE CODE ENDS HERE
            // -----------------------------------------------------------------------
        }

        /// <summary>
        /// Sends a url to Project Oxford and performs analysis
        /// </summary>
        /// <param name="imageUrl">The url of the image to analyze</param>
        /// <returns></returns>
        private async Task<AnalysisResult> AnalyzeUrl(string imageUrl)
        {
            // -----------------------------------------------------------------------
            // KEY SAMPLE CODE STARTS HERE
            // -----------------------------------------------------------------------

            //
            // Create Project Oxford Vision API Service client
            //
            VisionServiceClient VisionServiceClient = new VisionServiceClient(SubscriptionKey, SubscriptionEndpoint);
            Log("VisionServiceClient is created");

            //
            // Analyze the url for all visual features
            //
            Log("Calling VisionServiceClient.AnalyzeImageAsync()...");
            VisualFeature[] visualFeatures = new VisualFeature[] { VisualFeature.Adult, VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description, VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags };
            AnalysisResult analysisResult = await VisionServiceClient.AnalyzeImageAsync(imageUrl, visualFeatures);
            return analysisResult;

            // -----------------------------------------------------------------------
            // KEY SAMPLE CODE ENDS HERE
            // -----------------------------------------------------------------------
        }

        /// <summary>
        /// Perform the work for this scenario
        /// </summary>
        /// <param name="imageUri">The URI of the image to run against the scenario</param>
        /// <param name="upload">Upload the image to Project Oxford if [true]; submit the Uri as a remote url if [false];</param>
        /// <returns></returns>
        protected override async Task DoWork(Uri imageUri, bool upload)
        {
            _status.Text = "Analyzing...";

            //
            // Either upload an image, or supply a url
            //
            AnalysisResult analysisResult;
            if (upload)
            {
                analysisResult = await UploadAndAnalyzeImage(imageUri.LocalPath);
            }
            else
            {
                analysisResult = await AnalyzeUrl(imageUri.AbsoluteUri);
            }
            _status.Text = "Analyzing Done";

            //
            // Log analysis result in the log window
            //
            Log("");
            Log("Analysis Result:");
            LogAnalysisResult(analysisResult);
        }
    }
}
