﻿// Copyright (c) 2001-2016 Aspose Pty Ltd. All Rights Reserved.
//
// This file is part of Aspose.Words. The source code in this file
// is only intended as a supplement to the documentation, and is provided
// "as is", without warranty of any kind, either expressed or implied.
//////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using Aspose.Words;
using NUnit.Framework;

namespace ApiExamples
{
    [TestFixture]
    public class ExDigitalSignatureUtil : ApiExampleBase
    {
        [Test]
        public void RemoveAllSignaturesEx()
        {
            //ExStart
            //ExFor:DigitalSignatureUtil.RemoveAllSignatures(Stream, Stream)
            //ExFor:DigitalSignatureUtil.RemoveAllSignatures(String, String)
            //ExSummary:Shows how to remove every signature from a document.
            //By stream:
            Stream docStreamIn = new FileStream(MyDir + "Document.Signed.doc", FileMode.Open);
            Stream docStreamOut = new FileStream(MyDir + "Document.NoSignatures.FromStream.doc", FileMode.Create);

            DigitalSignatureUtil.RemoveAllSignatures(docStreamIn, docStreamOut);

            docStreamIn.Close();
            docStreamOut.Close();

            //By string:
            Document doc = new Document(MyDir + "Document.Signed.doc");
            string outFileName = MyDir + "Document.NoSignatures.FromString.doc";

            DigitalSignatureUtil.RemoveAllSignatures(doc.OriginalFileName, outFileName);
            //ExEnd
        }

        [Test]
        public void LoadSignaturesEx()
        {
            //ExStart
            //ExFor:DigitalSignatureUtil.LoadSignatures(Stream)
            //ExFor:DigitalSignatureUtil.LoadSignatures(String)
            //ExSummary:Shows how to load signatures from a document by stream and by string.
            Stream docStream = new FileStream(MyDir + "Document.Signed.doc", FileMode.Open);

            // By stream:
            DigitalSignatureCollection digitalSignatures = DigitalSignatureUtil.LoadSignatures(docStream);
            docStream.Close();

            // By string:
            digitalSignatures = DigitalSignatureUtil.LoadSignatures(MyDir + "Document.Signed.doc");
            //ExEnd
        }

        [Test]
        // We don't include a sample certificate with the examples
        // so this exception is expected instead since the file is not there.
        [ExpectedException(typeof(FileNotFoundException))]
        public void SignEx()
        {
            //ExStart
            //ExFor:DigitalSignatureUtil.Sign(String, String, CertificateHolder, String, DateTime)
            //ExFor:DigitalSignatureUtil.Sign(Stream, Stream, CertificateHolder, String, DateTime)
            //ExSummary:Shows how to sign documents.
            CertificateHolder ch = CertificateHolder.Create(MyDir + "MyPkcs12.pfx", "My password");

            //By String
            Document doc = new Document(MyDir + "Document.doc");
            string outputDocFileName = MyDir + "Document.Signed.doc";

            DigitalSignatureUtil.Sign(doc.OriginalFileName, outputDocFileName, ch, "My comment", DateTime.Now);

            //By Stream
            Stream docInStream = new FileStream(MyDir + "Document.doc", FileMode.Open);
            Stream docOutStream = new FileStream(MyDir + "Document.Signed.doc", FileMode.OpenOrCreate);

            DigitalSignatureUtil.Sign(docInStream, docOutStream, ch, "My comment", DateTime.Now);
            //ExEnd
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SingNull()
        {
            DigitalSignatureUtil.Sign(String.Empty, String.Empty, null, String.Empty, DateTime.Now, String.Empty);
        }

        //ToDo: Need to rework with certificate and add test which use stream
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SingWithPasswordDecrypring()
        {
            Document doc = new Document();

            // Create certificate holder from a file.
            CertificateHolder cert = CertificateHolder.Create(MyDir + "MyPkcs12.pfx", "My password");

            // Digitally sign encrypted with "docPassword" document in the specified path.
            DigitalSignatureUtil.Sign("srcDocFileName", "signedDocFileName", cert, "Comment", DateTime.Now, "docPassword");
            
            // Open encrypted document from a file.
            var signedDoc = new Document("signedDocFileName", new LoadOptions("docPassword"));

            // Check that encrypted document was successfully signed.
            DigitalSignatureCollection signatures = doc.DigitalSignatures;
            if (signatures.IsValid && (signatures.Count > 0))
            {
                Console.WriteLine("The document was signed successfully.");
            }
        }
    }
}