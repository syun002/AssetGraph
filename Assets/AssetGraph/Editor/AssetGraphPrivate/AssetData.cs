using UnityEngine;

using System;
using System.IO;
using D = System.Diagnostics;

namespace AssetGraph {
	public class AssetData {
		public readonly string traceId;
		public readonly string absoluteSourcePath;
		public readonly string sourceBasePath;
		public readonly string fileNameAndExtension;
		public readonly string pathUnderSourceBase;
		public readonly string importedPath;
		public readonly string assetId;
		
		/**
			pre-assets which is generated by Loaded.
		*/
		public static AssetData AssetDataByLoader (string absoluteSourcePath, string sourceBasePath) {
			return new AssetData(
				Guid.NewGuid().ToString(),
				absoluteSourcePath,
				sourceBasePath,
				Path.GetFileName(absoluteSourcePath),
				GetPathWithoutBasePath(absoluteSourcePath, sourceBasePath),
				null,
				null
			);
		}

		/**
			new assets which is Imported.
		*/
		public static AssetData AssetDataByImporter (string traceId, string absoluteSourcePath, string sourceBasePath, string fileNameAndExtension, string pathUnderSourceBase, string importedPath, string assetId) {
			return new AssetData(
				traceId,
				absoluteSourcePath,
				sourceBasePath,
				fileNameAndExtension,
				pathUnderSourceBase,
				importedPath,
				assetId
			);
		}

		/**
			new assets which is generated on Imported or Prefabricated or Bundlized.
		*/
		public static AssetData AssetDataGeneratedByImporterOrPrefabricatorOrBundlizer (string importedPath, string assetId) {
			return new AssetData(
				Guid.NewGuid().ToString(),
				null,
				null,
				Path.GetFileName(importedPath),
				null,
				importedPath,
				assetId
			);
		}

		private AssetData (
			string traceId,
			string absoluteSourcePath,
			string sourceBasePath,
			string fileNameAndExtension,
			string pathUnderSourceBase,
			string importedPath,
			string assetId
		) {
			this.traceId = traceId;
			this.absoluteSourcePath = absoluteSourcePath;
			this.sourceBasePath = sourceBasePath;
			this.fileNameAndExtension = fileNameAndExtension;
			this.pathUnderSourceBase = pathUnderSourceBase;
			this.importedPath = importedPath;
			this.assetId = assetId;
		}

		public static string GetPathWithoutBasePath (string localPathWithBasePath, string basePath) {
			var replaced = localPathWithBasePath.Replace(basePath, string.Empty);
			if (replaced.StartsWith(AssetGraphSettings.UNITY_FOLDER_SEPARATER)) return replaced.Substring(1);
			return replaced;
		}
		
		public static string GetPathWithBasePath (string localPathWithoutBasePath, string basePath) {
			return Path.Combine(basePath, localPathWithoutBasePath);
		}
	}
}