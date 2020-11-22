<?php
$pathPrefix = __DIR__ . DIRECTORY_SEPARATOR;

if($argc < 4){
	fwrite(STDERR, "Usage: {$argv[0]} $(outDir) $(TargetFramework) $(NuspecFile)\n");
	exit(1);
}

$outDir = rtrim($pathPrefix . $argv[1], "/\\");
$targetFramework = $argv[2];
$nuspecFile = $argv[3];

$dirIter = new RecursiveDirectoryIterator($outDir, FilesystemIterator::SKIP_DOTS);
$files = new RecursiveIteratorIterator($dirIter);

$dom = new DOMDocument();
$dom->formatOutput = true;
$dom->load($nuspecFile);

// remove previous
$filesNode = $dom->documentElement->getElementsByTagName('files')->item(0);
$dom->documentElement->removeChild($filesNode);
// create new
$filesNode = $dom->createElement('files');
$filesNode->appendChild($dom->createTextNode(PHP_EOL));

foreach($files as $f){
	$fi = pathinfo($f);

	$relaPath = str_replace($pathPrefix, '', $f);
	$relaDir = ltrim(str_replace($outDir, '', $fi['dirname']), "/\\");

	$targetPath = null;
	
	$extension = $fi['extension'] ?? '';
	switch($extension){
		case 'dll':
			$targetPath = "lib/{$targetFramework}/{$relaDir}" . $fi['basename'];
			break;
		default:
			$targetPath = "contentFiles/any/any/reko/{$relaDir}". $fi['basename'];
			break;
	}

	$fileNode = $dom->createElement('file');

	$fileNode->setAttribute('src', $relaPath);
	$fileNode->setAttribute('target', $targetPath);

	$filesNode->appendChild($fileNode);
	$filesNode->appendChild($dom->createTextNode(PHP_EOL));
}

$dom->documentElement->appendChild($filesNode);
$dom->save($nuspecFile);