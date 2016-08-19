using UnityEngine;
using System.Collections.Generic;
using UniLinq;
using UniRx;

public class HoldMiddle : MonoBehaviour {
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
	public PolygonCollider2D polygonCollider;

	public SpriteRenderer startLine;
	public SpriteRenderer endLine;

	void Start () {
		initPlane ();

		polygonCollider.CreatePrimitive (4);
		meshFilter.sharedMesh.bounds = new Bounds(Vector3.zero, new Vector3(100000, 100000, 100000));//適当
	}

	void Update(){
		List<Vector2> roundPositions = new List<Vector2> ();
		roundPositions.AddRange (getRoundPosition_top_and_bottom (startLine));
		roundPositions.AddRange (getRoundPosition_top_and_bottom (endLine));

		Mesh m = meshFilter.mesh;
		m.vertices = sortPosition_forVertices(roundPositions.ToArray());

		polygonCollider.SetPath (0, sortPosition_forPolygonCollider(m.vertices.Select (vector3 => (Vector2)vector3).ToArray()));
	}

	private List<Vector2> getRoundPosition_top_and_bottom(SpriteRenderer round){
		float round_r = round.sprite.bounds.size.y / 2;
		float position_x = round.gameObject.transform.localPosition.x;
		float position_y = round.gameObject.transform.localPosition.y;
		List<Vector2> vectors = new List<Vector2> ();
		vectors.Add (new Vector2 (position_x - round_r, position_y));
		vectors.Add (new Vector2 (position_x + round_r, position_y));
		return vectors;
	}

	void initPlane(){
		Mesh m = meshFilter.mesh;
		m.name = "newMesh";

		m.Clear ();

		m.vertices = new Vector3[4];


		int[] triangles = new int[]{
			0, 1, 2, //0,1,2の三角　右上＞左下＞左上の三角　時計回り側がポリゴン正面
			3, 1, 0 //3,1,0の三角　右下＞左下＞右上の三角　時計回り側がポリゴン正面
		};
		m.triangles = triangles;

		Vector2[] uv = new Vector2[]{//UVは左下原点が0,0
			new Vector2(1.0f,1.0f), // 右上の頂点のUV位置　1,1
			new Vector2(0.0f,0.0f), // 左下の頂点のUV位置　0,0
			new Vector2(0.0f,1.0f), // 左上の頂点のUV位置　0,1
			new Vector2(1.0f,0.0f) // 右下の頂点のUV位置　1,0
		};
		m.uv = uv;
	}

	Vector3[] sortPosition_forVertices(Vector2[] vectors){
		if (vectors.Length != 4) {
			Debug.LogWarning ("要素数は４つ必要です");
			return null;
		}
		//var sortVectors = vectors.Select (hoge => new{Vector = hoge}).OrderByDescending (hoge => hoge.Vector.x).ThenByDescending (hoge => hoge.Vector.y).ToArray ();
		Vector2[] sortVectors = vectors.OrderByDescending (vector => vector.x).ThenByDescending (vector => vector.y).ToArray ();

		Vector3[] returnVectors = new Vector3[]{ //Objectの原点を0,0,0として
			sortVectors [0], //右上の頂点位置
			sortVectors [3], //左下の頂点位置
			sortVectors [2], //左上の頂点位置
			sortVectors [1] //右下の頂点位置
		};
		return returnVectors;
	}
	Vector2[] sortPosition_forPolygonCollider(Vector2[] vectors){
		if (vectors.Length != 4) {
			Debug.LogWarning ("要素数は４つ必要です");
			return null;
		}
		//Vector2[] sortVectors = vectors.OrderByDescending (vector => vector.y).ThenByDescending (vector => vector.x).ToArray ();
		var sortVectors = vectors.OrderByDescending (vector => vector.y).ThenByDescending (vector => vector.x).ToArray ();
		Vector2[] returnVectors = new Vector2[]{
			sortVectors [0], //右上の頂点位置
			sortVectors [1], //右下の頂点位置
			sortVectors [3], //左下の頂点位置
			sortVectors [2], //左上の頂点位置
		};
		return returnVectors;
	}
}